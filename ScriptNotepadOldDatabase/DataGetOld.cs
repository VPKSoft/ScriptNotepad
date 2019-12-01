using System;
using System.Collections.Generic;
using System.Text;
using ScriptNotepadOldDatabase.Database.TableMethods;

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
    }
}
