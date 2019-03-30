﻿using ScriptNotepad.Database.Tables;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using ScriptNotepadPluginBase.PluginTemplateInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.PluginHandling
{
    /// <summary>
    /// A class for generating or updating a <see cref="PLUGINS"/> class which represents a table withing the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    public class PluginDatabaseEntry: ErrorHandlingBase
    {
        /// <summary>
        /// Creates an instance of a <see cref="PLUGINS"/> class to be inserted into the database.
        /// </summary>
        /// <param name="assembly">The assembly of the plug-in.</param>
        /// <param name="plugin">The initialized plug-in.</param>
        /// <param name="fileNameFull">The full file name of the plug-in assembly.</param>
        /// <returns>A PLUGINS class instance based on the given arguments.</returns>
        public static PLUGINS FromPlugin(Assembly assembly, IScriptNotepadPlugin plugin, string fileNameFull)
        {
            try
            {
                // create a result based on the given parameters..
                PLUGINS result = new PLUGINS()
                {
                    ID = -1,
                    FILENAME_FULL = fileNameFull,
                    FILENAME = Path.GetFileName(fileNameFull),
                    FILEPATH = Path.GetDirectoryName(fileNameFull),
                    PLUGIN_NAME = plugin.PluginName,
                    PLUGIN_DESCTIPTION = plugin.PluginDescription,
                    ISACTIVE = true,
                    PLUGIN_INSTALLED = DateTime.Now,
                };

                // set the version for the plug-in..
                result.SetPluginUpdated(assembly);

                // return the result..
                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates the plug-in entry data.
        /// </summary>
        /// <param name="pluginEntry">The plug-in entry <see cref="PLUGINS"/>.</param>
        /// <param name="assembly">The assembly of the plug-in.</param>
        /// <param name="plugin">The initialized plug-in.</param>
        /// <param name="fileNameFull">The full file name of the plug-in assembly.</param>
        /// <returns>An updated PLUGINS class instance based on the given arguments.</returns>
        public static PLUGINS UpdateFromPlugin(PLUGINS pluginEntry, Assembly assembly, IScriptNotepadPlugin plugin, string fileNameFull)
        {
            try
            {
                pluginEntry.FILENAME_FULL = fileNameFull;
                pluginEntry.FILENAME = Path.GetFileName(fileNameFull);
                pluginEntry.FILEPATH = Path.GetDirectoryName(fileNameFull);
                pluginEntry.PLUGIN_NAME = plugin.PluginName;
                pluginEntry.PLUGIN_DESCTIPTION = plugin.PluginDescription;

                // set the version for the plug-in..
                pluginEntry.SetPluginUpdated(assembly);
                return pluginEntry;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return pluginEntry;
            }
        }

        /// <summary>
        /// Return data acquired from an assembly in case a plug-in failed to initialize.
        /// </summary>
        /// <param name="assembly">The assembly of the plug-in.</param>
        /// <param name="fileNameFull">The full file name of the plug-in assembly.</param>
        /// <returns>A PLUGINS class instance based on the given arguments.</returns>
        public static PLUGINS InvalidPlugin(Assembly assembly, string fileNameFull)
        {
            try
            {
                string description = string.Empty;
                if (assembly != null)
                {
                    description = assembly.FullName;
                }

                // create a result based on the given parameters..
                PLUGINS result = new PLUGINS()
                {
                    ID = -1,
                    FILENAME_FULL = fileNameFull,
                    FILENAME = Path.GetFileName(fileNameFull),
                    FILEPATH = Path.GetDirectoryName(fileNameFull),
                    PLUGIN_NAME = "Unknown",
                    PLUGIN_DESCTIPTION = description,
                    ISACTIVE = false,
                    PLUGIN_INSTALLED = DateTime.Now,
                    LOAD_FAILURES = 1,
                };

                // set the version for the plug-in..
                result.SetPluginUpdated(assembly);

                // return the result..
                return result;
            }
            catch (Exception ex)
            {
                // log the exception..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }
    }
}