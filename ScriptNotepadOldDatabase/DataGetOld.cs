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
    }
}
