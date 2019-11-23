using System;
using SQLite.CodeFirst;

namespace ScriptNotepad.Database.Entity.Entities
{
    public class CustomHistory: IHistory
    {
        public int Id { get; set; }

        public string Hash { get; set; }

        public string Context { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
