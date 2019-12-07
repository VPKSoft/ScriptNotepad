using System;
using System.Collections.Generic;
using System.Text;
using ScriptNotepadOldDatabase.Database.TableCommands;
using ScriptNotepadOldDatabase.Database.TableMethods;
using VPKSoft.ScintillaLexers;

namespace ScriptNotepadOldDatabase
{
    public class DataGetOld
    {
        public static
            IEnumerable<(int Id, string FileNameFull, string SessionName, Encoding Encoding, string FileName, DateTime
                ClosedDateTime, string FilePath)> GetEntityDataRecentFiles(string connectionString)
        {
            return DatabaseRecentFiles.GetEntityData(connectionString);
        }

        public static
            IEnumerable<(int Id, string TextValue, int Type, DateTime Added, string SessionName)> GetEntityDataMiscText(
                string connectionString)
        {
            return DatabaseMiscText.GetEntityData(connectionString);
        }

        public static
            IEnumerable<(int Id, string SearchOrReplaceText, bool CaseSensitive, int SearchAndReplaceSearchType, int
                SearchAndReplaceType, DateTime Added, string FileSession)> GetEntityDataSearchAndReplace(string connectionString)
        {
            return DatabaseSearchAndReplace.GetEntityData(connectionString);
        }

        public static IEnumerable<(int Id, string ScriptContents, string ScriptName, DateTime Modified, int
                ScriptTextManipulationType)>
            GetEntityDataCodeSnippets(string connectionString)
        {
            return DatabaseCodeSnippets.GetEntityData(connectionString);
        }

        public static IEnumerable<(int Id, string FileNameFull, string FileName, string FilePath, string PluginName,
            string PluginVersion, string PluginDescription, bool IsActive, int ExceptionCount, int LoadFailures, int
            ApplicationCrashes, int SortOrder, int Rating, DateTime PluginInstalled, DateTime PluginUpdated, bool
            PendingDeletion)> GetEntityDataPlugins(string connectionString)
        {
            return DatabasePlugins.GetEntityData(connectionString);
        }

        public static IEnumerable<(int Id, Encoding Encoding, bool ExistsInFileSystem, string FileNameFull,
                string FileName, string FilePath, DateTime FileSystemModified, DateTime FileSystemSaved, DateTime
                DatabaseModified, LexerEnumerations.LexerType LexerType, byte[] FileContents, int VisibilityOrder, bool
                IsHistory, int CurrentCaretPosition, bool UseSpellChecking, int EditorZoomPercentage, int SessionId, bool IsActive, string SessionName)>
            GetEntityDataFileSave(string connectionString)
        {
            return DatabaseFileSave.GetEntityData(connectionString);
        }

        public static IEnumerable<(int Id, string SessionName)> GetEntityDataSession(string connectionString)
        {
            return DatabaseSessionName.GetEntityData(connectionString);
        }
    }
}
