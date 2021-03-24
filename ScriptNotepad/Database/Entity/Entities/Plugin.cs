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
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class to store plug-in data into the database.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
    [Table("Plugins")]
    public class Plugin : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full file name with path of the plug-in assembly.
        /// </summary>
        public string FileNameFull { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full file name without path of the plug-in assembly.
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the full path of the plug-in assembly.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the plug-in.
        /// </summary>
        public string PluginName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the version of the plug-in assembly.
        /// </summary>
        public string PluginVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the plug-in.
        /// </summary>
        public string PluginDescription { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the plug-in is active.
        /// </summary>
        public bool IsActive { get; set; } 

        /// <summary>
        /// Gets or sets the exception count the plug-in has reported via an event.
        /// </summary>
        public int ExceptionCount { get; set; }

        /// <summary>
        /// Gets or sets the amount of failed load attempts for the plug-in.
        /// </summary>
        public int LoadFailures { get; set; }

        /// <summary>
        /// Gets or sets the amount of how many times the plug-in has crashed the entire software.
        /// </summary>
        public int ApplicationCrashes { get; set; }

        /// <summary>
        /// Gets or sets the sort order for the plug-in during the load process.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the rating for the plug-in (0-100).
        /// </summary>
        public int Rating { get; set; } = 50;

        /// <summary>
        /// Gets or sets the date and time when the plug-in was installed.
        /// </summary>
        public DateTime PluginInstalled { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the plug-in was updated.
        /// </summary>
        public DateTime PluginUpdated { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the plug-in is pending for deletion from the plug-ins folder on application restart.
        /// </summary>
        public bool PendingDeletion { get; set; } 

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return PluginName + " / " + PluginDescription;
        }
    }
}

#nullable restore