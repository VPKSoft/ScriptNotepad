using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class to save the license of the software to the database.
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    public class SoftwareLicense: IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the license text.
        /// </summary>
        [Required]
        public string LicenseText { get; set; }

        /// <summary>
        /// Gets or sets the license SPDX identifier.
        /// See: https://spdx.org/licenses/
        /// </summary>
        [Required]
        public string LicenseSpdxIdentifier { get; set; }
    }
}
