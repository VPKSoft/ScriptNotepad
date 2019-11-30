using System;
using System.ComponentModel.DataAnnotations;
using ScriptNotepad.Database.Entity.Enumerations;

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class to store search or replace history into the database.
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
    public class SearchAndReplaceHistory : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the search or replace text.
        /// </summary>
        [Required]
        public string SearchOrReplaceText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether search or replace was case sensitive.
        /// </summary>
        [Required]
        public bool CaseSensitive { get; set; }

        /// <summary>
        /// Gets or sets the type of the search.
        /// </summary>
        [Required]
        public SearchAndReplaceSearchType SearchAndReplaceSearchType { get; set; }

        /// <summary>
        /// Gets or sets the type of the search or replace.
        /// </summary>
        [Required]
        public SearchAndReplaceType SearchAndReplaceType { get; set; }

        /// <summary>
        /// Gets or sets the added date and time when the entry was added to the database or created.
        /// </summary>
        [Required]
        public DateTime Added { get; set; }

        /// <summary>
        /// Gets or sets the session the search or replace history entry belongs to.
        /// </summary>
        [Required]
        public FileSession Session { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return SearchOrReplaceText;
        }
    }
}
