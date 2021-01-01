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

using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.Database.Entity.Model;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Context
{
    /// <summary>
    /// The database context for the ScriptNotepad <see cref="DbContext"/>.
    /// Implements the <see cref="System.Data.Entity.DbContext" />
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class ScriptNotepadDbContext: DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptNotepadDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string for a SQLite database.</param>
        public ScriptNotepadDbContext(string connectionString)
            : base(connectionString)
        {
            Configure();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptNotepadDbContext"/> class.
        /// </summary>
        /// <param name="connection">A database connection for the <see cref="DbContext"/>.</param>
        /// <param name="contextOwnsConnection">if set to <c>true</c> the <see cref="DbContext"/> owns the connection. I.e. the connection is disposed by the <see cref="DbContext"/>.</param>
        public ScriptNotepadDbContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {
            Configure();
        }

        /// <summary>
        /// A static property to hold the <see cref="ScriptNotepadDbContext"/> created with the <see cref="InitializeDbContext"/> method.
        /// </summary>
        /// <value>The database context.</value>
        public static ScriptNotepadDbContext DbContext { get; set; }

        /// <summary>
        /// Initializes the database <see cref="ScriptNotepadDbContext.DbContext"/> context.
        /// </summary>
        /// <param name="connectionString">The connection string to initialize the underlying SQLite database connection.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool InitializeDbContext(string connectionString)
        {
            SqLiteConnection = new SQLiteConnection(connectionString);
            SqLiteConnection.Open();

            try
            {
                DbContext = new ScriptNotepadDbContext(SqLiteConnection, false);
                DbContextInitialized = true;
                return true;
            }
            catch (Exception ex) // report the exception and return false..
            {
                DbContext = null;
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                DbContextInitialized = false;
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the SQLite connection for the Entity Framework Code First database.
        /// </summary>
        private static SQLiteConnection SqLiteConnection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the database context is initialized.
        /// </summary>
        /// <value><c>true</c> if the database is context initialized; otherwise, <c>false</c>.</value>
        internal static bool DbContextInitialized { get; set; }

        /// <summary>
        /// Releases the database <see cref="ScriptNotepadDbContext.DbContext"/> context.
        /// </summary>
        /// <param name="save">if set to <c>true</c> a the context is requested to save the changes before disposing of the context.</param>
        /// <param name="forceGarbageCollection">A value indicating whether to force the <see cref="SqLiteConnection"/> immediately to be garbage-collected.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool ReleaseDbContext(bool save = true, bool forceGarbageCollection = false)
        {
            try
            {
                if (DbContext != null) // null check..
                {
                    using (DbContext) // dispose of the context..
                    {
                        if (save) // ..if set to save, then save..
                        {
                            DbContext.SaveChanges();
                        }

                        DbContext = null; // set to null..
                    }

                    using (SqLiteConnection)
                    {
                        SqLiteConnection.Close();
                        SqLiteConnection = null;
                        if (forceGarbageCollection)
                        {
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                        }
                    }
                }
                DbContextInitialized = false;

                return true;
            }
            catch (Exception ex) // report the exception and return false..
            {
                ErrorHandlingBase.ExceptionLogAction?.Invoke(ex);
                DbContextInitialized = false;
                return false;
            }
        }

        /// <summary>
        /// Configures this <see cref="DbContext"/> class instance.
        /// </summary>
        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context. The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuilder, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.</remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ModelConfiguration.Configure(modelBuilder);
            base.OnModelCreating(modelBuilder);
//            var initializer = new ScriptNotepadDbInitializer(modelBuilder);
//            System.Data.Entity.Database.SetInitializer(initializer);
        }

        /// <summary>
        /// Gets or sets <see cref="FileSave"/> instances in the database.
        /// </summary>
        public DbSet<FileSave> FileSaves { get; set; }

        /// <summary>
        /// Gets or sets the file sessions used with other entity instances.
        /// </summary>
        public DbSet<FileSession> FileSessions { get; set; }

        /// <summary>
        /// Gets or sets <see cref="MiscellaneousTextEntry"/> instances in the database.
        /// </summary>
        public DbSet<MiscellaneousTextEntry> MiscellaneousTextEntries { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Plugin"/> instances in the database.
        /// </summary>
        public DbSet<Plugin> Plugins { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="RecentFile"/> instances in the database.
        /// </summary>
        public DbSet<RecentFile> RecentFiles { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CodeSnippet"/> instances in the database.
        /// </summary>
        public DbSet<CodeSnippet> CodeSnippets { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SearchAndReplaceHistory"/> instances in the database.
        /// </summary>
        public DbSet<SearchAndReplaceHistory> SearchAndReplaceHistories { get; set; }
    }
}
