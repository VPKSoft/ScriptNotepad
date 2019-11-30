#region License
/*
MIT License

Copyright(c) 2019 Petteri Kautonen

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
using System.Linq;
using ScriptNotepad.Database.Entity.Entities;
using SQLite.CodeFirst;
using VPKSoft.LangLib;

namespace ScriptNotepad.Database.Entity.Context
{
    /// <summary>
    /// Class ScriptNotepadDbInitializer.
    /// Implements the <see cref="ScriptNotepadDbContext" />
    /// </summary>
    /// <seealso cref="ScriptNotepadDbContext" />
    public class ScriptNotepadDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<ScriptNotepadDbContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptNotepadDbInitializer"/> class.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public ScriptNotepadDbInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder, typeof(CustomHistory))
        {
        }

        private const string SPDX_ID = @"MIT";

        /// <summary>
        /// The license of this software.
        /// </summary>
        private const string LICENSE =
            @"MIT License

Copyright(c) 2019 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.";
            

        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(ScriptNotepadDbContext context)
        {
            var defaultSessionName = DBLangEngine.GetStatMessage("msgDefaultSessionName",
                "Default|A name of the default session for the documents");

            var session = context.FileSessions.Find(1) ?? context.FileSessions.Add(new FileSession {Id = 1, SessionName = defaultSessionName});

            if (session != null)
            {
                // the user might have changed the locale of the software so change the name of to only constant
                // thing with it..
                session.SessionName = defaultSessionName;
            }

            // and a license is required - (semi-evil) laughs..
            if (!context.Set<SoftwareLicense>().Any())
            {
                context.Set<SoftwareLicense>().Add(new SoftwareLicense
                    {LicenseText = LICENSE, LicenseSpdxIdentifier = SPDX_ID});
            }

            // perhaps this puts things in motion..
            base.Seed(context);
        }
    }
}
