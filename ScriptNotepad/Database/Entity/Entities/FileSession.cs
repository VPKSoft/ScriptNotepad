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

#nullable enable

using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Entities
{
    /// <summary>
    /// A class for storing session(s) into the database.
    /// </summary>
    [Table("FileSessions")]
    public class FileSession: ErrorHandlingBase, IEntity
    {
        /// <summary>
        /// Gets or sets the identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of a session.
        /// </summary>
        public string? SessionName { get; set; }

        /// <summary>
        /// Gets a value indicating whether this session instance is the default session.
        /// </summary>
        [NotMapped]
        public bool IsDefault => Id == 1;

        /// <summary>
        /// Gets or sets the temporary file path in case the file system is used to cache the contents
        /// of the <see cref="FileSave"/> entities belonging to the session.
        /// </summary>
        public string? TemporaryFilePath { get; set; }

        /// <summary>
        /// Gets or sets the application data directory for caching files in case the <see cref="UseFileSystemOnContents"/> property is set to true.
        /// </summary>
        public static string ApplicationDataDirectory { get; set; } = string.Empty;

        /// <summary>
        /// Generates, creates and sets a random path for the <see cref="TemporaryFilePath"/> property in case the property value is null.
        /// </summary>
        /// <returns>The generated or already existing path for temporary files for the session.</returns>
        public string? SetRandomPath()
        {
            if (TemporaryFilePath == null && UseFileSystemOnContents)
            {
                var path = Path.Combine(ApplicationDataDirectory, Path.GetRandomFileName());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                TemporaryFilePath = path;

                return path;
            }

            if (!UseFileSystemOnContents)
            {
                TemporaryFilePath = null;
            }

            return TemporaryFilePath;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the file system to store the contents of the file instead of a database BLOB.
        /// </summary>
        public bool UseFileSystemOnContents { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string? ToString()
        {
            return SessionName;
        }
    }
}

#nullable restore
