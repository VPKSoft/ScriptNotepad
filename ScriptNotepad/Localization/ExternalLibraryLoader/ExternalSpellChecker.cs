using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using VPKSoft.ErrorLogger;
using VPKSoft.ScintillaSpellCheck;
using VPKSoft.SpellCheck.ExternalDictionarySource;

namespace ScriptNotepad.Localization.ExternalLibraryLoader
{
    /// <summary>
    /// If a spell checking for a certain language is provided via 
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class ExternalSpellChecker: ErrorHandlingBase
    {
        /// <summary>
        /// Loads a spell checker library from a given path with a given file name.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void LoadSpellCheck(string path, string fileName)
        {
            try
            {
                fileName = Path.Combine(path, fileName);

                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                // the location of the assembly must be defined..
                Assembly spellCheck = Assembly.LoadFile(@"C:\Files\GitHub\VoikkoSharp\VoikkoSharpTestApp\bin\Debug\VoikkoSharp.dll");

                SetSpellChecker(spellCheck);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
            }
        }

        /// <summary>
        /// Handles the AssemblyResolve event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ResolveEventArgs"/> instance containing the event data.</param>
        /// <returns>Assembly.</returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // parts of the code borrowed from (C): https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders

            // ignore resources..
            if (args.Name.Contains(".resources"))
            {
                return null;
            }

            // check for assemblies already loaded..
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
            {
                return assembly;
            }

            // Try to load by filename - split out the filename of the full assembly name
            // and append the base path of the original assembly (ie. look in the same dir)
            string filename = args.Name.Split(',')[0] + ".dll".ToLower();

            filename = Path.Combine(@"C:\Files\GitHub\VoikkoSharp\VoikkoSharpTestApp\bin\Debug", filename);

            try
            {
                return Assembly.LoadFrom(filename);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Initializes an external spell checker from a specified <see cref="Assembly"/>
        /// </summary>
        /// <param name="assembly">The assembly to load the spell checker from.</param>
        public static void SetSpellChecker(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // again keep on trying..
                try
                {
                    // check the validity of the found type..
                    if (typeof(IExternalDictionarySource).IsAssignableFrom(type))
                    {
                        ScintillaSpellCheck.ExternalDictionary = (IExternalDictionarySource)Activator.CreateInstance(type);
                        ScintillaSpellCheck.ExternalDictionary.Initialize();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogAction?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// Disposes of the resources used by the spell checking library.
        /// </summary>
        public static void DisposeResources()
        {
            ScintillaSpellCheck.ExternalDictionary?.Dispose();
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
        }
    }
}
