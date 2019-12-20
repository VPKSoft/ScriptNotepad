namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class representing a single file contents in the database.
    /// Implements the <see cref="ScriptNotepad.Database.Entity.IEntity" />
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.Entity.IEntity" />
    public class FileContent: IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the binary contents of a file.
        /// </summary>
        public byte[] BinaryContents { get; set; }
    }
}
