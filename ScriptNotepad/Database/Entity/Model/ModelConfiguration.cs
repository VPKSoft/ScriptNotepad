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

using System.Data.Entity;
using ScriptNotepad.Database.Entity.Entities;

namespace ScriptNotepad.Database.Entity.Model
{
    /// <summary>
    /// A class to configure the Code First Entity Framework model.
    /// </summary>
    public class ModelConfiguration
    {
        /// <summary>
        /// Configures the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void Configure(DbModelBuilder modelBuilder)
        {
            ConfigureSimpleEntity<FileSave>(modelBuilder);
            ConfigureSimpleEntity<FileSession>(modelBuilder);
            ConfigureSimpleEntity<Plugin>(modelBuilder);
            ConfigureSimpleEntity<MiscellaneousTextEntry>(modelBuilder);
            ConfigureSimpleEntity<RecentFile>(modelBuilder);
            ConfigureSimpleEntity<CodeSnippet>(modelBuilder);
            ConfigureSimpleEntity<SearchAndReplaceHistory>(modelBuilder);
            ConfigureSimpleEntity<SoftwareLicense>(modelBuilder);
        }

        /// <summary>
        /// Configures a simple entity.
        /// </summary>
        /// <typeparam name="T">The entity type.</typeparam>
        /// <param name="modelBuilder">The model builder to use for the configuration.</param>
        private static void ConfigureSimpleEntity<T>(DbModelBuilder modelBuilder) where T : class
        {
            modelBuilder.Entity<T>();
        }
    }
}
