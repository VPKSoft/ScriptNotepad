using System.Data.Common;
using System.Data.Entity;
using ScriptNotepad.Database.Entity.Model;

namespace ScriptNotepad.Database.Entity.Context
{
    public class ScriptNotepadDbContext: DbContext
    {
        public ScriptNotepadDbContext(string connectionString)
            : base(connectionString)
        {
            Configure();
        }

        public ScriptNotepadDbContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
            var initializer = new ScriptNotepadDbInitializer(modelBuilder);
            System.Data.Entity.Database.SetInitializer(initializer);
        }
    }
}
