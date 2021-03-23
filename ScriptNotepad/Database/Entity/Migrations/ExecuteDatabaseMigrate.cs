#region License
/*
MIT License

Copyright(c) 2021 Petteri Kautonen

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
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

// This sample (C): https://fluentmigrator.github.io/articles/quickstart.html?tabs=runner-in-process
namespace ScriptNotepad.Database.Entity.Migrations
{
    /// <summary>
    /// A class to run the database migrations.
    /// </summary>
    public static class ExecuteDatabaseMigrate
    {
        /// <summary>
        /// Migrates the database changes with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The SQLite connection string.</param>
        public static void Migrate(string connectionString)
        {
            ConnectionString = connectionString;

            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using var scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        /// <summary>
        /// Gets or sets the name for the default session in the database.
        /// </summary>
        /// <value>The name for the default session in the database.</value>
        public static string DefaultSessionName { get; set; } = @"Default";

        /// <summary>
        /// Gets or sets the SQLite database connection string.
        /// </summary>
        /// <value>The the SQLite database connection string.</value>
        private static string ConnectionString { get; set; }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // ReSharper disable once CommentTypo
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // ReSharper disable once CommentTypo
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString(ConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
                // ReSharper disable once CommentTypo
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
