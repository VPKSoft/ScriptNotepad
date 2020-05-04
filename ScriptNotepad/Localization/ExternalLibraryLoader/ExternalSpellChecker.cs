#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ScriptNotepad.Settings;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using VPKSoft.ErrorLogger;
using VPKSoft.ExternalDictionaryPackage;
using VPKSoft.ScintillaSpellCheck;
using VPKSoft.SpellCheck.ExternalDictionarySource;

namespace ScriptNotepad.Localization.ExternalLibraryLoader
{
    /// <summary>
    /// If a spell checking for a certain language is provided via an external assembly/library this class can be used to load such library.
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

                var assemblyName = Path.Combine(path, fileName);

                // the location of the assembly must be defined..
                Assembly spellCheck = Assembly.LoadFile(assemblyName);

                SetSpellChecker(spellCheck);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
            }
        }

        /// <summary>
        /// Loads a custom spell checking assembly if defined in the settings file.
        /// </summary>
        public static void Load()
        {
            if (FormSettings.Settings.EditorSpellUseCustomDictionary)
            {
                try
                {
                    var data = DictionaryPackage.GetXmlDefinitionDataFromDefinitionFile(FormSettings.Settings
                        .EditorSpellCustomDictionaryDefinitionFile);
                    ExternalSpellChecker.LoadSpellCheck(Path.GetDirectoryName(FormSettings.Settings
                        .EditorSpellCustomDictionaryDefinitionFile), data.lib);

                }
                catch (Exception ex)
                {
                    // log the exception..
                    ExceptionLogAction?.Invoke(ex);
                }
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
            // parts of the code borrowed from; Thanks To (C): https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders

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

            filename = Path.Combine(
                Path.GetDirectoryName(FormSettings.Settings.EditorSpellCustomDictionaryDefinitionFile) ?? string.Empty,
                filename);

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
