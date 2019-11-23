using System.Data.Entity;
using ScriptNotepad.Database.Entity.Entities;
using SQLite.CodeFirst;

namespace ScriptNotepad.Database.Entity.Context
{
    public class ScriptNotepadDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<ScriptNotepadDbContext>
    {
        public ScriptNotepadDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder, typeof(CustomHistory))
        {
        }

        protected override void Seed(ScriptNotepadDbContext context)
        {
            // Here you can seed your core data if you have any.
        }
    }
}
