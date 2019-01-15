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

using System;
using System.IO;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaTabbedTextControl;

namespace ScriptNotepad.Database
{
    /// <summary>
    /// Represents a file saved to the database.
    /// </summary>
    public class DBFILE_SAVE
    {
        /// <summary>
        /// Gets or sets the ID number (database).
        /// </summary>
        public long ID { get; set; } = -1;

        /// <summary>
        /// Gets or sets a value indicating whether the file exists in the file system.
        /// </summary>
        public bool EXISTS_INFILESYS { get; set; } = false;

        /// <summary>
        /// Gets or sets the full file name with path.
        /// </summary>
        public string FILENAME_FULL { get; set; }

        /// <summary>
        /// Gets or sets the file name without path.
        /// </summary>
        public string FILENAME { get; set; }

        /// <summary>
        /// Gets or sets the full path for the file.
        /// </summary>
        public string FILEPATH { get; set; }

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the file system.
        /// </summary>
        public DateTime FILESYS_MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the value indicating when the file was modified in the database.
        /// </summary>
        public DateTime DB_MODIFIED { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the lexer number with the ScintillaNET.
        /// </summary>
        public LexerType LEXER_CODE { get; set; }

        /// <summary>
        /// Gets or sets the file contents.
        /// </summary>
        public MemoryStream FILE_CONTENTS { get; set; }

        /// <summary>
        /// Gets or sets the visibility order (in a tabbed control).
        /// </summary>
        public int VISIBILITY_ORDER { get; set; } = -1;

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public int SESSIONID { get; set; } = 1;

        /// <summary>
        /// Gets or sets the name of the session.
        /// </summary>
        public string SESSIONNAME { get; set; } = "Default";

        /// <summary>
        /// Gets or sets a value indicating whether if the file is activated in the tab control.
        /// </summary>
        public bool ISACTIVE { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entry is a history entry.
        /// </summary>
        public bool ISHISTORY { get; set; }

        /// <summary>
        /// Disposes the memory stream containing the document's contents.
        /// </summary>
        public void DisposeMemoryStream()
        {
            if (FILE_CONTENTS != null)
            {
                using (FILE_CONTENTS)
                {
                    FILE_CONTENTS = null;
                }
            }
        }

        /// <summary>
        /// Reloads the contents of the document from the disk.
        /// </summary>
        /// <param name="document">A ScintillaTabbedDocument to which contents should also be updated.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        public bool ReloadFromDisk(ScintillaTabbedDocument document)
        {
            try
            {
                // dispose of the previous file contents..
                DisposeMemoryStream();

                // can't reload what doesn't exist..
                if (File.Exists(FILENAME_FULL))
                {
                    // read the file contents from the file..
                    using (FileStream fileStream = new FileStream(FILENAME_FULL, FileMode.Open, FileAccess.Read))
                    {
                        // create a byte buffer the contain all the bytes if the file with an assumption
                        // no one wishes to open massive binary files..
                        byte[] fileContents = new byte[fileStream.Length];

                        // read the file contents to the buffer..
                        fileStream.Read(fileContents, 0, (int)fileStream.Length);

                        // create a new memory stream to hold the file contents..
                        FILE_CONTENTS = new MemoryStream(fileContents);

                        // set the file system's modified flag..
                        FILESYS_MODIFIED = new FileInfo(FILENAME_FULL).LastWriteTime;

                        // read the file's contents to the ScintillaNET control..
                        using (StreamReader streamReader = new StreamReader(FILE_CONTENTS))
                        {
                            FILE_CONTENTS.Position = 0; // position the stream..
                            document.Scintilla.Text = streamReader.ReadToEnd(); // read all to the Scintilla..
                            FILE_CONTENTS.Position = 0; // reposition the stream..
                        }
                    }
                    return true; // success..
                }
                else
                {
                    return false; // the file didn't exists, so fail..
                }
            }
            catch
            {
                return false; // an exception occurred, so fail..
            }
        }

        // a value indicating if the user want's to reload the changes from the file system if the file has been changed..
        private bool _ShouldQueryDiskReload = true;

        private static bool DateTimeLarger(DateTime dt1, DateTime dt2)
        {
            string s1 = Database.DateToDBString(dt1);
            string s2 = Database.DateToDBString(dt2);
            return s1.CompareTo(s2) > 0;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be queried of to reload the changed document from the file system.
        /// </summary>
        public bool ShouldQueryDiskReload
        {
            get
            {
                // note to self: "I do hate this logic with date and time!"..

                // get the last time the file was written into..
                DateTime dtUpdated = new FileInfo(FILENAME_FULL).LastWriteTime;

                // get the result to be returned..
                bool result = _ShouldQueryDiskReload && DateTimeLarger(dtUpdated, FILESYS_MODIFIED);// dtUpdated > FILESYS_MODIFIED;

                // reset this flag so the user can be annoyed again with a stupid question of reloading the file..
                // .. after rethinking, don't do this:  _ShouldQueryDiskReload = true;

                // return the result if the file has been changed in the file system..
                return result;
            }

            set
            {
                // set the flag whether the user want's to hear the
                // stupid question of reloading the file on the next time..
                _ShouldQueryDiskReload = value;
            }
        }
    }
}
