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
using System.Reflection;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Utility
{
    /// <summary>
    /// A class to help to deal with <see cref="Assembly"/> versions.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class AssemblyVersion: ErrorHandlingBase
    {
        /// <summary>
        /// Gets a version string from a given <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly to set the version from.</param>
        public static string VersionStringFromAssembly(Assembly assembly)
        {
            try
            {
                // return the version from the given assembly..
                return assembly.GetName().Version.ToString();
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);

                // return a default value..
                return "1.0.0.0";
            }
        }

        /// <summary>
        /// Sets the <see cref="Plugin.PluginVersion"/> from a given assembly.
        /// </summary>
        /// <param name="plugin">The plugin which version to update.</param>
        /// <param name="assembly">The assembly to set the version from.</param>
        public static void VersionFromAssembly(Plugin plugin, Assembly assembly)
        {
            // get the plug-in version from the given assembly..
            plugin.PluginVersion = VersionStringFromAssembly(assembly);
        }

        /// <summary>
        /// Sets the <see cref="Plugin.PluginUpdated"/> property if the given assembly version is larger than the previous <see cref="Plugin.PluginVersion"/>.
        /// </summary>
        /// <param name="plugin">The plugin which update state to check.</param>
        /// <param name="assembly">The assembly which version to compare to the current <see cref="Plugin.PluginVersion"/> one.</param>
        public static void SetPluginUpdated(Plugin plugin, Assembly assembly)
        {
            try
            {
                Version newVersion = assembly.GetName().Version; // get the assembly version..
                Version previousVersion = new Version(plugin.PluginVersion); // get the previous version..

                // update the version whether required or not..
                VersionFromAssembly(plugin, assembly);


                // if the new version is larger than the previous one..
                if (newVersion > previousVersion)
                {
                    // ..set a new time for the PLUGIN_UPDATED property..
                    plugin.PluginUpdated = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
            }
        }
    }
}
