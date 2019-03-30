﻿#region License
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

using ScriptNotepad.Database.TableCommands;
using ScriptNotepad.Database.Tables;
using ScriptNotepad.UtilityClasses.StreamHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using VPKSoft.ScintillaLexers;
using VPKSoft.ScintillaTabbedTextControl;
using static ScriptNotepad.Database.DatabaseEnumerations;

namespace ScriptNotepad.Database.TableMethods
{
    /// <summary>
    /// A class containing methods for database interaction with the DBFILE_SAVE table.
    /// </summary>
    /// <seealso cref="ScriptNotepad.Database.Database" />
    public class DatabaseFileSave: Database
    {
        /// <summary>
        /// Adds a given DBFILE_SAVE class instance into the database cache.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be added into the database.</param>
        /// <returns>A DBFILE_SAVE class instance file was successfully added to the database; otherwise null.</returns>
        public static DBFILE_SAVE AddFile(DBFILE_SAVE fileSave)
        {
            int recordsAffected = 0;
            try
            {
                string sql = DatabaseCommandsFileSave.GenInsertFileSentence(fileSave);

                fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ?
                    new FileInfo(fileSave.FILENAME_FULL).LastWriteTime :
                    DateTime.MinValue;

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    // add parameters to the command..

                    // add the contents of the Scintilla.NET document as a parameter..
                    command.Parameters.Add("@FILE", System.Data.DbType.Binary).Value = fileSave.FILE_CONTENTS.ToArray();

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
        /// Adds a given file into the database cache.
        /// </summary>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="sessionName">A name of the session to which the document should be saved to.</param>
        /// <param name="databaseHistoryFlag">An enumeration indicating how to behave with the <see cref="DBFILE_SAVE"/> class ISHISTORY flag.</param>
        /// <param name="encoding">An encoding for the document.</param>
        /// <param name="ID">An unique identifier for the file.</param>
        /// <returns>A DBFILE_SAVE class instance file was successfully added to the database; otherwise null.</returns>
        public static DBFILE_SAVE AddFile(ScintillaTabbedDocument document, DatabaseHistoryFlag databaseHistoryFlag, string sessionName, Encoding encoding, int ID = -1)
        {
            try
            {
                DBFILE_SAVE fileSave = new DBFILE_SAVE()
                {
                    ID = document.ID,
                    EXISTS_INFILESYS = File.Exists(document.FileName),
                    FILENAME_FULL = document.FileName,
                    FILENAME = Path.GetFileName(document.FileName),
                    FILEPATH = Path.GetDirectoryName(document.FileName),
                    FILESYS_MODIFIED = File.Exists(document.FileName) ?
                        new FileInfo(document.FileName).LastWriteTime :
                        DateTime.MinValue,
                    DB_MODIFIED = DateTime.Now,
                    LEXER_CODE = document.LexerType,
                    FILE_CONTENTS = StreamStringHelpers.TextToMemoryStream(document.Scintilla.Text, encoding),
                    VISIBILITY_ORDER = (int)document.FileTabButton.Tag,
                    SESSIONNAME = sessionName,
                    ISACTIVE = document.FileTabButton.IsActive,
                    ENCODING = encoding,
                    ISHISTORY = databaseHistoryFlag == DatabaseHistoryFlag.IsHistory // in a database sense only the value if IsHistory is true..
                };

                return AddFile(fileSave);
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
        public static bool UpdateFileHistoryFlag(DBFILE_SAVE fileSave)
        {
            return ExecuteArbitrarySQL(DatabaseCommandsFileSave.GenUpdateFileHistoryFlag(fileSave));
        }

        /// <summary>
        /// Adds or updates a a given file into the database cache.
        /// </summary>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <param name="sessionName">A name of the session to which the document should be saved to.</param>
        /// <param name="databaseHistoryFlag">An enumeration indicating how to behave with the <see cref="DBFILE_SAVE"/> class ISHISTORY flag.</param>
        /// <param name="encoding">An encoding for the document.</param>
        /// <param name="ID">An unique identifier for the file.</param>
        /// <returns>An instance to a DBFILE_SAVE class if the operations was successful; otherwise null;</returns>
        public static DBFILE_SAVE AddOrUpdateFile(ScintillaTabbedDocument document, DatabaseHistoryFlag databaseHistoryFlag, string sessionName, Encoding encoding, int ID = -1)
        {
            return UpdateFile(AddFile(document, databaseHistoryFlag, sessionName, encoding, ID));
        }

        /// <summary>
        /// Adds or updates a a given file into the database cache.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be added or updated into the database.</param>
        /// <param name="document">An instance to a ScintillaTabbedDocument class.</param>
        /// <returns>An instance to a DBFILE_SAVE class if the operations was successful; otherwise null;</returns>
        public static DBFILE_SAVE AddOrUpdateFile(DBFILE_SAVE fileSave, ScintillaTabbedDocument document)
        {
            fileSave.FILE_CONTENTS = StreamStringHelpers.TextToMemoryStream(document.Scintilla.Text, fileSave.ENCODING);
            return UpdateFile(AddFile(fileSave));
        }

        /// <summary>
        /// Updates a given file to the database cache.
        /// </summary>
        /// <param name="fileSave">A DBFILE_SAVE class instance to be updated into the database.</param>
        /// <returns>A modified instance of the DBFILE_SAVE if the operation was successful; otherwise null;</returns>
        public static DBFILE_SAVE UpdateFile(DBFILE_SAVE fileSave)
        {
            string sql = DatabaseCommandsFileSave.GenUpdateFileSentence(ref fileSave);
            try
            {
                fileSave.FILESYS_MODIFIED = File.Exists(fileSave.FILENAME_FULL) ?
                    new FileInfo(fileSave.FILENAME_FULL).LastWriteTime :
                    DateTime.MinValue;

                // as the SQLiteCommand is disposable a using clause is required..
                using (SQLiteCommand command = new SQLiteCommand(conn))
                {
                    command.CommandText = sql;
                    // add parameters to the command..

                    // add the contents of the Scintilla.NET document as a parameter..
                    command.Parameters.Add("@FILE", DbType.Binary).Value = fileSave.FILE_CONTENTS.ToArray();

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
        /// <returns>A DBFILE_SAVE class instance if the operation was successful; otherwise null.</returns>
        public static DBFILE_SAVE GetFileFromDatabase(string sessionName, string fileNameFull)
        {
            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsFileSave.GenDocumentSelect(sessionName, DatabaseHistoryFlag.DontCare, fileNameFull), conn))
            {
                using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    if (reader.Read())
                    {
                        return FromDataReader(reader);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets a <see cref="DBFILE_SAVE"/> class instance from a <see cref="SQLiteDataReader"/> class instance.
        /// </summary>
        /// <param name="reader">The <see cref="SQLiteDataReader"/> class instance to read the data from.</param>
        /// <returns>A DBFILE_SAVE class instance if the operation was successful; otherwise null.</returns>
        public static DBFILE_SAVE FromDataReader(SQLiteDataReader reader)
        {
            try
            {
                // ID: 0, EXISTS_INFILESYS: 1, FILENAME_FULL: 2, FILENAME: 3, FILEPATH: 4,
                // FILESYS_MODIFIED: 5, DB_MODIFIED: 6, LEXER_CODE: 7, FILE_CONTENTS: 8,
                // VISIBILITY_ORDER: 9, SESSIONID: 10, ISACTIVE: 11, ISHISTORY: 12, SESSIONNAME: 13
                // FILESYS_SAVED: 14, ENCODING: 15
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
                        LEXER_CODE = (LexerType)reader.GetInt32(7), // cast to a lexer type..
                        FILE_CONTENTS = MemoryStreamFromBlob(reader.GetBlob(8, true)),
                        VISIBILITY_ORDER = reader.GetInt32(9),
                        SESSIONID = reader.GetInt32(10),
                        ISACTIVE = reader.GetInt32(11) == 1,
                        ISHISTORY = reader.GetInt32(12) == 1,
                        SESSIONNAME = reader.GetString(13),
                        FILESYS_SAVED = DateFromDBString(reader.GetString(14)),
                        ENCODING = Encoding.GetEncoding(reader.GetString(15))
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
        /// <param name="sessionName">Name of the session with the saved file snapshots belong to.</param>
        /// <param name="databaseHistoryFlag">An enumeration indicating how to behave with the <see cref="DBFILE_SAVE"/> class ISHISTORY flag.</param>
        /// <returns>A collection of DBFILE_SAVE class instances matching the given parameters.</returns>
        public static IEnumerable<DBFILE_SAVE> GetFilesFromDatabase(string sessionName, DatabaseHistoryFlag databaseHistoryFlag)
        {
            List<DBFILE_SAVE> result = new List<DBFILE_SAVE>();

            using (SQLiteCommand command = new SQLiteCommand(DatabaseCommandsFileSave.GenDocumentSelect(sessionName, databaseHistoryFlag), conn))
            {
                // can't get the BLOB without this (?!)..
                using (SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (reader.Read())
                    {
                        result.Add(FromDataReader(reader));
                    }
                }
            }

            return result;
        }
    }
}