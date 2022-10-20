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

using Microsoft.EntityFrameworkCore;
using ScriptNotepad.Database.Entity.Entities;
using ScriptNotepad.UtilityClasses.ErrorHandling;

namespace ScriptNotepad.Database.Entity.Context;

/// <summary>
/// The database context for the ScriptNotepad <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
/// Implements the <see cref="Microsoft.EntityFrameworkCore.DbContext" />
/// </summary>
/// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
public class ScriptNotepadDbContext: DbContext
{
    private static string ConnectionString { get; set; } = "ScriptNotepadEntityCore.sqlite";

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptNotepadDbContext"/> class.
    /// </summary>
    public ScriptNotepadDbContext()
    {
        ConnectionString ??= "ScriptNotepadEntityCore.sqlite";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScriptNotepadDbContext"/> class.
    /// </summary>
    /// <param name="connectionString">The connection string for a SQLite database.</param>
    public ScriptNotepadDbContext(string connectionString)
    {
        ScriptNotepadDbContext.ConnectionString = connectionString;
    }

    /// <summary>
    /// <para>
    /// Override this method to configure the database (and other options) to be used for this context.
    /// This method is called for each instance of the context that is created.
    /// The base implementation does nothing.
    /// </para>
    /// <para>
    /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
    /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
    /// the options have already been set, and skip some or all of the logic in
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
    /// </para>
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
    /// typically define extension methods on this object that allow you to configure the context.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(ConnectionString);
        base.OnConfiguring(optionsBuilder);
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
        try
        {
            DbContext = new ScriptNotepadDbContext(connectionString);
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
    /// Gets or sets a value indicating whether the database context is initialized.
    /// </summary>
    /// <value><c>true</c> if the database is context initialized; otherwise, <c>false</c>.</value>
    internal static bool DbContextInitialized { get; set; }

    /// <summary>
    /// Releases the database <see cref="ScriptNotepadDbContext.DbContext"/> context.
    /// </summary>
    /// <param name="save">if set to <c>true</c> a the context is requested to save the changes before disposing of the context.</param>
    /// <param name="forceGarbageCollection">A value indicating whether to force the <see cref="DbContext"/> instance immediately to be garbage-collected.</param>
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

                if (forceGarbageCollection)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
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

    /// <summary>
    /// Gets or sets the <see cref="MiscellaneousParameters"/> instances in the database.
    /// </summary>
    /// <value>The <see cref="MiscellaneousParameters"/> instances in the database.</value>
    public DbSet<MiscellaneousParameter> MiscellaneousParameters { get; set; }
}