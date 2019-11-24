using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptNotepad.Database.Entity.Utility;
using ScriptNotepad.Database.Tables;
using ScriptNotepad.UtilityClasses.ErrorHandling;
using static ScriptNotepad.Database.Entity.Utility.AssemblyVersion;

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class to store plug-in data into the database.
    /// Implements the <see cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.UtilityClasses.ErrorHandling.ErrorHandlingBase" />
    /// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
    public class Plugin : ErrorHandlingBase, IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full file name with path of the plug-in assembly.
        /// </summary>
        [Required]
        public string FileNameFull { get; set; }

        /// <summary>
        /// Gets or sets the full file name without path of the plug-in assembly.
        /// </summary>
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the full path of the plug-in assembly.
        /// </summary>
        [Required]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the plug-in.
        /// </summary>
        [Required]
        public string PluginName { get; set; }

        /// <summary>
        /// Gets or sets the version of the plug-in assembly.
        /// </summary>
        [Required]
        public string PluginVersion { get; set; } 

        /// <summary>
        /// Gets or sets the description of the plug-in.
        /// </summary>
        public string PluginDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the plug-in is active.
        /// </summary>
        [Required]
        public bool IsActive { get; set; } 

        /// <summary>
        /// Gets or sets the exception count the plug-in has reported via an event.
        /// </summary>
        [Required]
        public int ExceptionCount { get; set; } = 0;

        /// <summary>
        /// Gets or sets the amount of failed load attempts for the plug-in.
        /// </summary>
        [Required]
        public int LoadFailures { get; set; } = 0;

        /// <summary>
        /// Gets or sets the amount of how many times the plug-in has crashed the entire software.
        /// </summary>
        [Required]
        public int ApplicationCrashes { get; set; } = 0;

        /// <summary>
        /// Gets or sets the sort order for the plug-in during the load process.
        /// </summary>
        [Required]
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// Gets or sets the rating for the plug-in (0-100).
        /// </summary>
        [Required]
        public int Rating { get; set; } = 50;

        /// <summary>
        /// Gets or sets the date and time when the plug-in was installed.
        /// </summary>
        [Required]
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
        /// Gets a value indicating whether this <see cref="FileNameFull"/> exists in the file system.
        /// </summary>
        public bool Exists => File.Exists(FileNameFull);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            return PluginName + " / " + PluginDescription;
        }

        /// <summary>
        /// Sets the <see cref="PluginVersion"/> from a given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to set the version from.</param>
        private void VersionFromAssembly(Assembly assembly)
        {
            // get the plug-in version from the given assembly..
            PluginVersion = VersionStringFromAssembly(assembly);
        }

        /// <summary>
        /// Sets the <see cref="PluginUpdated"/> property if the given assembly version is larger than the previous <see cref="PluginVersion"/>.
        /// </summary>
        /// <param name="assembly">The assembly which version to compare to the current <see cref="PluginVersion"/> one.</param>

        public void SetPluginUpdated(Assembly assembly)
        {
            AssemblyVersion.SetPluginUpdated(this, assembly);
        }
    }
}
