#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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

using ScriptNotepadPluginBase.PluginTemplateInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VPKSoft.ErrorLogger;

namespace ScriptNotepad.PluginHandling
{
    /// <summary>
    /// A class witch searches a given directory for possible plug-in assemblies.
    /// </summary>
    public static class PluginDirectoryRoaming
    {
        /// <summary>
        /// Gets the plug-in assemblies for the software.
        /// </summary>
        /// <param name="directory">The directory to search the plug-in assemblies from.</param>
        /// <returns>A collection of tuples containing the information of the found assemblies.</returns>
        public static IEnumerable<(Assembly Assembly, string Path, bool IsValid)> GetPluginAssemblies(string directory)
        {
            // create a list for the results..
            List<(Assembly Assembly, string Path, bool IsValid)> result = new List<(Assembly Assembly, string Path, bool IsValid)>();

            try
            {
                // recurse the plug-in path..
                string[] assemblies = Directory.GetFiles(directory, "*.dll", SearchOption.AllDirectories);

                // loop through the results..
                foreach (string assemblyFile in assemblies)
                {
                    // some other files (.dll_blaa) might come with the *.dll mask..
                    if (Path.GetExtension(assemblyFile).ToLowerInvariant() != ".dll".ToLowerInvariant())
                    {
                        // ..in that case do continue..
                        continue;
                    }

                    // this might also fail so try..
                    try
                    {
                        // load the found assembly..
                        Assembly assembly = Assembly.LoadFile(assemblyFile);

                        foreach (Type type in assembly.GetTypes())
                        {
                            // again keep on trying..
                            try
                            {
                                // check the validity of the found type..
                                if (typeof(IScriptNotepadPlugin).IsAssignableFrom(type))
                                {
                                    // create an instance of the class implementing the IScriptNotepadPlugin interface..
                                    IScriptNotepadPlugin plugin =
                                        (IScriptNotepadPlugin)Activator.CreateInstance(type);

                                    // the IScriptNotepadPlugin is also disposable, so do dispose of it..
                                    using (plugin)
                                    {
                                        result.Add((assembly, assemblyFile, true));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // log the exception..
                                ExceptionLogger.LogError(ex);

                                // indicate a failure in the result as well..
                                result.Add((assembly, assemblyFile, false));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionLogger.LogError(ex);

                        // indicate a failure in the result as well..
                        result.Add((null, assemblyFile, false));
                    }
                }
            }
            // a failure..
            catch (Exception ex)
            {
                // ..so do log it..
                ExceptionLogger.LogError(ex);
            }

            // return the result..
            return result;
        }
    }
}
