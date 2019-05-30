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

using ScriptNotepad.UtilityClasses.ErrorHandling;
using System;
using System.IO;
using System.Reflection;

namespace ScriptNotepad.Database.Tables
{
    /// <summary>
    /// A class representing the PLUGINS table in the database.
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase"/>
    public class PLUGINS: ErrorHandlingBase
    {
        /// <summary>
        /// Gets or sets the ID number of the entry in the PLUGINS database table.
        /// </summary>
        public long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets the full file name with path of the plug-in assembly.
        /// </summary>
        public string FILENAME_FULL { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full file name without path of the plug-in assembly.
        /// </summary>
        public string FILENAME { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full path of the plug-in assembly.
        /// </summary>
        public string FILEPATH { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the plug-in.
        /// </summary>
        public string PLUGIN_NAME { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version of the plug-in assembly.
        /// </summary>
        public string PLUGIN_VERSION { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the plug-in.
        /// </summary>
        public string PLUGIN_DESCTIPTION { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the plug-in is active.
        /// </summary>
        public bool ISACTIVE { get; set; } = false;

        /// <summary>
        /// Gets or sets the exception count the plug-in has reported via an event.
        /// </summary>
        public int EXCEPTION_COUNT { get; set; } = 0;

        /// <summary>
        /// Gets or sets the amount of failed load attempts for the plug-in.
        /// </summary>
        public int LOAD_FAILURES { get; set; } = 0;

        /// <summary>
        /// Gets or sets the amount of how many times the plug-in has crashed the entire software.
        /// </summary>
        public int APPLICATION_CRASHES { get; set; } = 0;

        /// <summary>
        /// Gets or sets the sort order for the plug-in during the load process.
        /// </summary>
        public int SORTORDER { get; set; } = 0;

        /// <summary>
        /// Gets or sets the rating for the plug-in (0-100).
        /// </summary>
        public int RATING { get; set; } = 50;

        /// <summary>
        /// Gets or sets the date and time when the plug-in was installed.
        /// </summary>
        public DateTime PLUGIN_INSTALLED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the date and time when the plug-in was updated.
        /// </summary>
        public DateTime PLUGIN_UPDATED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets a flag indicating if the plug-in is pending for deletion from the plug-ins folder on application restart.
        /// </summary>
        public bool PENDING_DELETION { get; set; } = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="PLUGINS"/> exists in the file system.
        /// </summary>
        public bool Exists { get => File.Exists(FILENAME_FULL); }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return PLUGIN_NAME + " / " + PLUGIN_DESCTIPTION;
        }

        /// <summary>
        /// Sets the <see cref="PLUGIN_VERSION"/> from a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to set the version from.</param>
        private void VersionFromAssembly(Assembly assembly)
        {
            // get the plug-in version from the given assembly..
            PLUGIN_VERSION = VersionStringFromAssembly(assembly);
        }

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
        /// Sets the <see cref="PLUGIN_UPDATED"/> property if the given assembly version is larger than the previous <see cref="PLUGIN_VERSION"/>.
        /// </summary>
        /// <param name="assembly">The assembly which version to compare to the current <see cref="PLUGIN_VERSION"/> one.</param>
        public void SetPluginUpdated(Assembly assembly)
        {
            try
            {
                Version newVersion = assembly.GetName().Version; // get the assembly version..
                Version previousVersion = new Version(PLUGIN_VERSION); // get the previous version..

                // update the version whether required or not..
                VersionFromAssembly(assembly);


                // if the new version is larger than the previous one..
                if (newVersion > previousVersion)
                {
                    // ..set a new time for the PLUGIN_UPDATED property..
                    PLUGIN_UPDATED = DateTime.Now;
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
