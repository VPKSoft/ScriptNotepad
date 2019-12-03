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
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.Tables;
using ScriptNotepadOldDatabase.Database.UtilityClasses.StreamHelpers;
using ScriptNotepadOldDatabase.UtilityClasses.Encodings;
using VPKSoft.LangLib;
using VPKSoft.ScintillaLexers;

namespace ScriptNotepadOldDatabase.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the DBFILE_SAVE table.
    /// </summary>
    /// <seealso cref="Database" />
    internal class DatabaseFileSave: Database
    {
        /// <summary>
        /// Adds a given DBFILE_SAVE class instance into the database cache.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be added into the database.</param>
        /// <returns>A DBFILE_SAVE class instance file was successfully added to the database; otherwise null.</returns>
        internal static DBFILE_SAVE AddFile(DBFILE_SAVE fileSave)
        {
            try
            {
                string sql = DatabaseCommandsFileSave.GenInsertFileSentence(fileSave);

                fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ?
                    new FileInfo(fileSave.FILENAME_FULL).LastWriteTime :
                    DateTime.MinValue;

                // as the SQLiteCommand is disposable a using clause is required..
                int recordsAffected;
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;
                    // add parameters to the command..

                    // add the contents of the Scintilla.NET document as a parameter..
                    command.Parameters.Add("@FILE", DbType.Binary).Value = 
                        StreamStringHelpers.TextToMemoryStream(fileSave.FILE_CONTENTS, fileSave.ENCODING).ToArray();

                    // do the insert..
                    recordsAffected = command.ExecuteNonQuery();
                }

                long id = GetScalar<long>(DatabaseCommandsMisc.GenGetCurrentIDForTable("DBFILE_SAVE"));

                fileSave.ID = (recordsAffected > 0 && fileSave.ID == -1) ? id : fileSave.ID;

                // no negative ID number is accepted..
                if (fileSave.ID == -1)
                {
                    fileSave.ID = GetScalar<long>(DatabaseCommandsFileSave.GetExistingDBFileSaveIDSentence(fileSave));
                }

                return fileSave;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates the file's ISHISTORY flag in the database.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance which ISHISTORY flag to update to the database.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        internal static bool UpdateFileHistoryFlag(DBFILE_SAVE fileSave)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsFileSave.GenUpdateFileHistoryFlag(fileSave));
        }

        /// <summary>
        /// Updates a given file to the database cache.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be updated into the database.</param>
        /// <param name="currentPosition">The current caret position of the document.</param>
        /// <returns>A modified instance of the DBFILE_SAVE if the operation was successful; otherwise null;</returns>
        internal static DBFILE_SAVE UpdateFile(DBFILE_SAVE fileSave, int currentPosition)
        {
            if (fileSave == null)
            {
                return null;
            }

            string sql = DatabaseCommandsFileSave.GenUpdateFileSentence(ref fileSave);
            try
            {
                fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ?
                    new FileInfo(fileSave.FILENAME_FULL).LastWriteTime :
                    DateTime.MinValue;

                fileSave.CURRENT_POSITION = currentPosition;

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(Connection))
                {
                    command.CommandText = sql;
                    // add parameters to the command..

                    // add the contents of the Scintilla.NET document as a parameter..
                    command.Parameters.Add("@FILE", DbType.Binary).Value = StreamStringHelpers.TextToMemoryStream(fileSave.FILE_CONTENTS, fileSave.ENCODING).ToArray();

                    // do the insert..
                    command.ExecuteNonQuery();
                }
                return fileSave;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
                return null;
            }
        }

        /// <summary>
        /// Gets a single file from the database with given parameters.
        /// </summary>
        /// <param name="sessionName">Name of the session with the saved file snapshots belong to.</param>
        /// <param name="fileNameFull">The full file name of the file to get from the database.</param>
        /// <param name="getZoom">A flag indicating whether to get the saved zoom value from the database.</param>
        /// <returns>A DBFILE_SAVE class instance if the operation was successful; otherwise null.</returns>
        internal static DBFILE_SAVE GetFileFromDatabase(string sessionName, string fileNameFull, bool getZoom)
        {
            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsFileSave.GenDocumentSelect(sessionName, DatabaseEnumerations.DatabaseHistoryFlag.DontCare, fileNameFull), Connection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    if (reader.Read())
                    {
                        return FromDataReader(reader, getZoom);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Validates if a snapshot of a file exists in the DBFILE_SAVE database table.
        /// </summary>
        /// <param name="sessionName">Name of the session to which the file is supposed to belong to.</param>
        /// <param name="fileNameFull">The full file name of the file.</param>
        /// <returns><c>true</c> if a file snapshot exists in the database, <c>false</c> otherwise.</returns>
        internal static bool FileExistsInDatabase(string sessionName, string fileNameFull)
        {
            return GetScalar<long>(DatabaseCommandsFileSave.GenIfExistsInDatabase(sessionName, fileNameFull)) !=
                   default;
        }

        /// <summary>
        /// Gets the encoding for a file snapshot from the database.
        /// </summary>
        /// <param name="sessionName">Name of the session to which the file is supposed to belong to.</param>
        /// <param name="fileNameFull">The full file name of the file.</param>
        /// <returns>An <see cref="Encoding"/> class instance if the operation was successful; otherwise null.</returns>
        internal static Encoding GetEncodingFromDatabase(string sessionName, string fileNameFull)
        {
            try
            {
                Encoding fileSaveEncoding = null;
                using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsFileSave.GetEncodingFromDatabase(sessionName, fileNameFull), Connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // ENCODING = 0, UNICODE_BOM = 1, UNICODE_BIGENDIAN = 2
                        if (reader.Read())
                        {
                            fileSaveEncoding = EncodingData.EncodingFromString(reader.GetString(0));

                            // unicode (UTFxxx) is a special encoding (because of BOM, etc)..
                            if (fileSaveEncoding.CodePage == Encoding.UTF8.CodePage)
                            {
                                fileSaveEncoding = new UTF8Encoding(reader.GetInt32(1) == 1);
                            }
                            else if (fileSaveEncoding.CodePage == Encoding.Unicode.CodePage)
                            {
                                fileSaveEncoding = new UnicodeEncoding(reader.GetInt32(2) == 1, reader.GetInt32(1) == 0);
                            }
                            else if (fileSaveEncoding.CodePage == Encoding.UTF32.CodePage)
                            {
                                fileSaveEncoding = new UTF32Encoding(reader.GetInt32(2) == 1, reader.GetInt32(1) == 0);
                            }
                        }
                    }
                }

                return fileSaveEncoding;
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
            }

            return null;
        }

        /// <summary>
        /// Updates the file name of a given <see cref="DBFILE_SAVE"/> class instance which doesn't exist in the file system.
        /// </summary>
        /// <param name="fileSave">An instance to a <see cref="DBFILE_SAVE"/> class.</param>
        /// <param name="previousName">The previous name of the non-existing file.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        internal static bool UpdateFileName(DBFILE_SAVE fileSave, string previousName)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsFileSave.GenRenameNewFile(fileSave, previousName));
        }

        /// <summary>
        /// Updates the miscellaneous flags of a given <see cref="DBFILE_SAVE"/> class instance.
        /// </summary>
        /// <param name="fileSave">An instance to a <see cref="DBFILE_SAVE"/> class.</param>
        /// <returns>True if the operation was successful; otherwise false.</returns>
        internal static bool UpdateMiscFlags(DBFILE_SAVE fileSave)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsFileSave.GenUpdateFileMiscFlags(fileSave));
        }

        /// <summary>
        /// Gets all the data to from the table convert to Entity Framework.
        /// </summary>
        /// <param name="connectionString">A SQLite database connection string.</param>
        /// <returns>System.Collections.Generic.IEnumerable&lt;(System.Int32 Id, System.Text.Encoding Encoding, System.Boolean ExistsInFileSystem, System.String FileNameFull, System.String FileName, System.String FilePath, System.DateTime FileSystemModified, System.DateTime FileSystemSaved, System.DateTime DatabaseModified, VPKSoft.ScintillaLexers.LexerEnumerations.LexerType LexerType, System.Byte[] FileContents, System.Int32 VisibilityOrder, System.Boolean IsHistory, System.Int32 CurrentCaretPosition, System.Boolean UseSpellChecking, System.Int32 EditorZoomPercentage, System.Int32 SessionId, System.Boolean IsActive)&gt;.</returns>
        internal static IEnumerable<(int Id, Encoding Encoding, bool ExistsInFileSystem, string FileNameFull,
                        string FileName, string FilePath, DateTime FileSystemModified, DateTime FileSystemSaved, DateTime
                        DatabaseModified, LexerEnumerations.LexerType LexerType, byte[] FileContents, int VisibilityOrder, bool
                        IsHistory, int CurrentCaretPosition, bool UseSpellChecking, int EditorZoomPercentage, int SessionId, bool IsActive, string SessionName)>
                    GetEntityData(string connectionString)
        {
            InitConnection(connectionString);

            using (var sqLiteConnection = new SQLiteConnection(connectionString))
            {
                var fileSaves = GetFilesFromDatabase();
                foreach (var fileSave in fileSaves)
                {
                    var legacy = fileSave;
                    
                    yield return ((int)legacy.ID, legacy.ENCODING, legacy.EXISTS_INFILESYS, legacy.FILENAME_FULL,
                            legacy.FILENAME, legacy.FILEPATH, legacy.FILESYS_MODIFIED, legacy.FILESYS_SAVED,
                            legacy.DB_MODIFIED, legacy.LEXER_CODE, legacy.ENCODING.GetBytes(legacy.FILE_CONTENTS), legacy.VISIBILITY_ORDER,
                            legacy.ISHISTORY, legacy.CURRENT_POSITION, legacy.USESPELL_CHECK, legacy.EDITOR_ZOOM, (int)legacy.SESSIONID, legacy.ISACTIVE, legacy.SESSIONNAME);
                }
            }

            using (Connection)
            {
                // dispose of the connection..
            }
        }

        /// <summary>
        /// Gets a <see cref="DBFILE_SAVE"/> class instance from a <see cref="SQLiteDataReader"/> class instance.
        /// </summary>
        /// <param name="reader">The <see cref="SQLiteDataReader"/> class instance to read the data from.</param>
        /// <param name="getZoom">A flag indicating whether to get the saved zoom value from the database.</param>
        /// <returns>A DBFILE_SAVE class instance if the operation was successful; otherwise null.</returns>
        internal static DBFILE_SAVE FromDataReader(SQLiteDataReader reader, bool getZoom)
        {
            try
            {
                // ID: 0, EXISTS_INFILESYS: 1, FILENAME_FULL: 2, FILENAME: 3, FILEPATH: 4,
                // FILESYS_MODIFIED: 5, DB_MODIFIED: 6, LEXER_CODE: 7, FILE_CONTENTS: 8,
                // VISIBILITY_ORDER: 9, SESSIONID: 10, ISACTIVE: 11, ISHISTORY: 12, SESSIONNAME: 13,
                // FILESYS_SAVED: 14, ENCODING: 15, CURRENT_POSITION = 16, USESPELL_CHECK = 17,
                // EDITOR_ZOOM = 18

                Encoding fileSaveEncoding = EncodingData.EncodingFromString(reader.GetString(15));

                return
                    new DBFILE_SAVE()
                    {
                        ID = reader.GetInt64(0),
                        EXISTS_INFILESYS = reader.GetInt32(1) == 1,
                        FILENAME_FULL = reader.GetString(2),
                        FILENAME = reader.GetString(3),
                        FILEPATH = reader.GetString(4),
                        FILESYS_MODIFIED = DateFromDBString(reader.GetString(5)),
                        DB_MODIFIED = DateFromDBString(reader.GetString(6)),
                        LEXER_CODE = (LexerEnumerations.LexerType)reader.GetInt32(7), // cast to a lexer type..
                        FILE_CONTENTS = StreamStringHelpers.MemoryStreamToText(MemoryStreamFromBlob(reader.GetBlob(8, true)), fileSaveEncoding),
                        VISIBILITY_ORDER = reader.GetInt32(9),
                        SESSIONID = reader.GetInt32(10),
                        ISACTIVE = reader.GetInt32(11) == 1,
                        ISHISTORY = reader.GetInt32(12) == 1,
                        SESSIONNAME = reader.GetString(13),
                        FILESYS_SAVED = DateFromDBString(reader.GetString(14)),
                        ENCODING = EncodingData.EncodingFromString(reader.GetString(15)),
                        CURRENT_POSITION = reader.GetInt32(16),
                        USESPELL_CHECK = reader.GetInt32(17) == 1,
                        EDITOR_ZOOM = getZoom ? reader.GetInt32(18) : 100,
                    };
            }
            catch (Exception ex)
            {
                // log the exception if the action has a value..
                ExceptionLogAction?.Invoke(ex);
            }
            return null;
        }

        /// <summary>
        /// Gets the file snapshots from the database.
        /// </summary>
        /// <returns>A collection of DBFILE_SAVE class instances matching the given parameters.</returns>
        internal static IEnumerable<DBFILE_SAVE> GetFilesFromDatabase()
        {
            List<DBFILE_SAVE> result = new List<DBFILE_SAVE>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsFileSave.GenDocumentSelect(), Connection))
            {
                // can't get the BLOB without this (?!)..
                using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (reader.Read())
                    {
                        result.Add(FromDataReader(reader, true));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the file snapshots from the database.
        /// </summary>
        /// <param name="sessionName">Name of the session with the saved file snapshots belong to.</param>
        /// <param name="databaseHistoryFlag">An enumeration indicating how to behave with the <see cref="DBFILE_SAVE"/> class ISHISTORY flag.</param>
        /// <param name="getZoom">A flag indicating whether to get the saved zoom value from the database.</param>
        /// <returns>A collection of DBFILE_SAVE class instances matching the given parameters.</returns>
        internal static IEnumerable<DBFILE_SAVE> GetFilesFromDatabase(string sessionName, DatabaseEnumerations.DatabaseHistoryFlag databaseHistoryFlag, bool getZoom)
        {
            List<DBFILE_SAVE> result = new List<DBFILE_SAVE>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsFileSave.GenDocumentSelect(sessionName, databaseHistoryFlag), Connection))
            {
                // can't get the BLOB without this (?!)..
                using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (reader.Read())
                    {
                        result.Add(FromDataReader(reader, getZoom));
                    }
                }
            }

            return result;
        }
    }
}
