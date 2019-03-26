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
using System.Reflection;
using System.Windows.Forms;
using VPKSoft.ErrorLogger;
using static ScriptNotepadPluginBase.Types.DelegateTypes;

namespace ScriptNotepad.PluginHandling
{
    /// <summary>
    /// A helper class for the plug-in loading and initialization.
    /// </summary>
    public static class PluginInitializer
    {
        /// <summary>
        /// Tries to loads a plug-in with a given file name.
        /// </summary>
        /// <param name="fileName">Name of the file containing the plug-in assembly.</param>
        /// <returns>A tuple containing the assembly and an instance created for the plug-in implementing 
        /// the <see cref="IScriptNotepadPlugin"/> interface along with the assembly file name if successful; 
        /// otherwise the resulting value contains some null values.</returns>
        public static (Assembly assembly, IScriptNotepadPlugin Plugin, string FileName) LoadPlugin(string fileName)
        {
            try
            {
                // load the found assembly..
                Assembly assembly = Assembly.LoadFile(fileName);

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

                            return (assembly, plugin, fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        // log the exception..
                        ExceptionLogger.LogError(ex);

                        // indicate a failure in the result as well..
                        return (assembly, null, fileName);
                    }
                }

                // a class type implementing the IScriptNotepadPlugin interface wasn't found..
                return (assembly, null, fileName);
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);
                return (null, null, fileName);
            }
        }

        /// <summary>
        /// Initializes the given plug-in instance.
        /// </summary>
        /// <param name="plugin">The plug-in instance to initialize.</param>
        /// <param name="onRequestActiveDocument">The event provided by the hosting software (ScriptNotepad) to request for the active document within the software.</param>
        /// <param name="onRequestAllDocuments">The event provided by the hosting software (ScriptNotepad) to request for all open documents within the software.</param>
        /// <param name="onPluginException">The event provided by the hosting software (ScriptNotepad) for error reporting.</param>
        /// <param name="pluginMenuStrip">The <see cref="ToolStripMenuItem"/> which is the plug-in menu in the hosting software (ScriptNotepad).</param>
        /// <param name="sessionName">The name of the current session in the hosting software (ScriptNotepad).</param>
        /// <param name="formMain">A reference to the main form of the hosting software (ScriptNotepad).</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public static bool InitializePlugin(IScriptNotepadPlugin plugin,
            OnRequestActiveDocument onRequestActiveDocument,
            OnRequestAllDocuments onRequestAllDocuments,
            OnPluginException onPluginException,
            ToolStripMenuItem pluginMenuStrip,
            string sessionName,
            FormMain formMain
            )
        {
            try
            {
                // initialize the plug-in..
                plugin.Initialize(onRequestActiveDocument, onRequestAllDocuments, onPluginException, pluginMenuStrip, sessionName, formMain);
                return true; // success..
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogger.LogError(ex);

                return false; // fail..
            }
        }
    }
}
