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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using FluentMigrator;

namespace DatabaseMigrations
{
    /// <summary>
    /// Initial database migration for the ScriptNotepad software.
    /// Implements the <see cref="FluentMigrator.Migration" />
    /// </summary>
    /// <seealso cref="FluentMigrator.Migration" />
    [Migration(20210101103253)]
    public class InitialMigration: Migration
    {
        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        public override void Up()
        {
            // the CodeSnippets table..
            Create.Table("CodeSnippets")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("ScriptContents").AsString().Nullable()
                .WithColumn("ScriptName").AsString().NotNullable()
                .WithColumn("Modified").AsDateTime().NotNullable()
                .WithColumn("ScriptLanguage").AsInt32().NotNullable()
                .WithColumn("ScriptTextManipulationType").AsInt32().NotNullable();

            // the FileSessions table..
            Create.Table("FileSessions")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("SessionName").AsString().Unique().Nullable()
                .WithColumn("TemporaryFilePath").AsString().Nullable()
                .WithColumn("UseFileSystemOnContents").AsBoolean().NotNullable();

            // the FileSaves table..
            Create.Table("FileSaves")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("EncodingAsString").AsString().NotNullable()
                .WithDefaultValue("'utf-8;65001;True;False;False'")
                .WithColumn("ExistsInFileSystem").AsBoolean().NotNullable()
                .WithColumn("FileNameFull").AsString().NotNullable()
                .WithColumn("FileName").AsString().NotNullable()
                .WithColumn("FilePath").AsString().Nullable()
                .WithColumn("FileSystemModified").AsDateTime().NotNullable()
                .WithColumn("FileSystemSaved").AsDateTime().NotNullable()
                .WithColumn("DatabaseModified").AsDateTime().NotNullable()
                .WithColumn("LexerType").AsInt32().NotNullable()
                .WithColumn("UseFileSystemOnContents").AsBoolean().Nullable()
                .WithColumn("TemporaryFileSaveName").AsString().Nullable()
                .WithColumn("FileContents").AsBinary().Nullable()
                .WithColumn("VisibilityOrder").AsInt32().NotNullable().WithDefaultValue(-1)
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("IsHistory").AsBoolean().NotNullable()
                .WithColumn("CurrentCaretPosition").AsInt32().NotNullable()
                .WithColumn("UseSpellChecking").AsBoolean().NotNullable()
                .WithColumn("EditorZoomPercentage").AsInt32().NotNullable().WithDefaultValue(100)
                .WithColumn("Session_Id").AsInt64().ForeignKey("FK_FileSaves_FileSessions_Id", "FileSessions", "Id")
                .OnDelete(Rule.Cascade).NotNullable();

            // an index to the FileSaves table..
            Create.Index("IX_FileSave_Session_Id").OnTable("FileSaves").OnColumn("Session_Id");

            // the MiscellaneousTextEntries table..
            Create.Table("MiscellaneousTextEntries")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("TextValue").AsString().NotNullable()
                .WithColumn("TextType").AsInt32().NotNullable()
                .WithColumn("Added").AsDateTime().NotNullable().WithDefaultValue("DATETIME('now', 'localtime')")
                .WithColumn("Session_Id").AsInt64().ForeignKey("FK_MiscellaneousTextEntries_FileSessions_Id", "FileSessions", "Id")
                .OnDelete(Rule.Cascade).NotNullable();

            // an index to the MiscellaneousTextEntries table..
            Create.Index("IX_MiscellaneousTextEntry_Session_Id").OnTable("MiscellaneousTextEntries").OnColumn("Session_Id");

            // the Plugins table..
            Create.Table("Plugins")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("FileNameFull").AsString().NotNullable()
                .WithColumn("FileName").AsString().NotNullable()
                .WithColumn("FilePath").AsString().NotNullable()
                .WithColumn("PluginName").AsString().NotNullable()
                .WithColumn("PluginVersion").AsString().NotNullable()
                .WithColumn("PluginDescription").AsString().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("ExceptionCount").AsInt32().NotNullable()
                .WithColumn("LoadFailures").AsInt32().NotNullable()
                .WithColumn("ApplicationCrashes").AsInt32().NotNullable()
                .WithColumn("SortOrder").AsInt32().NotNullable()
                .WithColumn("Rating").AsInt32().NotNullable()
                .WithColumn("PluginInstalled").AsDateTime().NotNullable()
                .WithColumn("PluginUpdated").AsDateTime().NotNullable()
                .WithColumn("PendingDeletion").AsBoolean().NotNullable();

            // the RecentFiles table..
            Create.Table("RecentFiles")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("FileNameFull").AsString().NotNullable()
                .WithColumn("FileName").AsString().NotNullable()
                .WithColumn("FilePath").AsString().Nullable()
                .WithColumn("ClosedDateTime").AsDateTime().NotNullable()
                .WithColumn("EncodingAsString").AsString().NotNullable()
                .WithDefaultValue("'utf-8;65001;True;False;False'")
                .WithColumn("Session_Id").AsInt64().ForeignKey("FK_RecentFiles_FileSessions_Id", "FileSessions", "Id")
                .OnDelete(Rule.Cascade).NotNullable();

            // an index to the RecentFiles table..
            Create.Index("IX_RecentFile_Session_Id").OnTable("RecentFiles").OnColumn("Session_Id");

            // the RecentSearchAndReplaceHistoriesFiles table..
            Create.Table("SearchAndReplaceHistories")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("SearchOrReplaceText").AsString().NotNullable()
                .WithColumn("CaseSensitive").AsBoolean().NotNullable()
                .WithColumn("SearchAndReplaceSearchType").AsInt32().NotNullable()
                .WithColumn("SearchAndReplaceType").AsInt32().NotNullable()
                .WithColumn("Added").AsDateTime().NotNullable()
                .WithColumn("Session_Id").AsInt64().ForeignKey("FK_SearchAndReplaceHistories_FileSessions_Id", "FileSessions", "Id")
                .OnDelete(Rule.Cascade).NotNullable();

            // an index to the RecentSearchAndReplaceHistoriesFiles table..
            Create.Index("IX_SearchAndReplaceHistory_Session_Id").OnTable("SearchAndReplaceHistories").OnColumn("Session_Id");

            // the SoftwareLicenses table..
            Create.Table("SoftwareLicenses")
                .WithColumn("Id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("LicenseText").AsString().NotNullable()
                .WithColumn("LicenseSpdxIdentifier").AsString().NotNullable();

            // seed with some constant data..
            Insert.IntoTable("SoftwareLicenses").Row(new ConcurrentDictionary<string, object>(
                new List<KeyValuePair<string, object>>(new[]
                {
                    new KeyValuePair<string, object>("LicenseText", License),
                    new KeyValuePair<string, object>("LicenseSpdxIdentifier", SPDX_ID)
                })));

            Insert.IntoTable("CodeSnippets").Row(new ConcurrentDictionary<string, object>(
                new List<KeyValuePair<string, object>>(new[]
                {
                    new KeyValuePair<string, object>("ScriptName",  @"Simple replace script"),
                    new KeyValuePair<string, object>("ScriptContents", SimpleReplaceScript),
                    new KeyValuePair<string, object>("Modified", DateTime.Now),
                    new KeyValuePair<string, object>("ScriptLanguage", 0),
                    new KeyValuePair<string, object>("ScriptTextManipulationType", 1),
                })));

            Insert.IntoTable("CodeSnippets").Row(new ConcurrentDictionary<string, object>(
                new List<KeyValuePair<string, object>>(new[]
                {
                    new KeyValuePair<string, object>("ScriptName",  @"Simple line ending change script"),
                    new KeyValuePair<string, object>("ScriptContents", SimpleLineEndingChangeScript),
                    new KeyValuePair<string, object>("Modified", DateTime.Now),
                    new KeyValuePair<string, object>("ScriptLanguage", 0),
                    new KeyValuePair<string, object>("ScriptTextManipulationType", 0),
                })));

            Insert.IntoTable("CodeSnippets").Row(new ConcurrentDictionary<string, object>(
                new List<KeyValuePair<string, object>>(new[]
                {
                    new KeyValuePair<string, object>("ScriptName", @"Simple XML manipulation script"),
                    new KeyValuePair<string, object>("ScriptContents", SimpleXmlManipulationScript),
                    new KeyValuePair<string, object>("Modified", DateTime.Now),
                    new KeyValuePair<string, object>("ScriptLanguage", 0),
                    new KeyValuePair<string, object>("ScriptTextManipulationType", 0),
                })));

            Insert.IntoTable("FileSessions").Row(new ConcurrentDictionary<string, object>(
                new List<KeyValuePair<string, object>>(new[]
                {
                    new KeyValuePair<string, object>("SessionName", ExecuteDatabaseMigrate.DefaultSessionName),
                    new KeyValuePair<string, object>("UseFileSystemOnContents", false),
                })));
        }

        /// <summary>
        /// Collects the DOWN migration expressions
        /// </summary>
        public override void Down()
        {
            // delete the CodeSnippets table..
            Delete.Table("CodeSnippets");

            // delete the FileSessions table..
            Delete.Table("FileSessions");

            // delete the FileSaves table..
            Delete.Index("IX_FileSave_Session_Id");
            Delete.Table("FileSaves");

            // delete the MiscellaneousTextEntries table..
            Delete.Index("IX_MiscellaneousTextEntry_Session_Id");
            Delete.Table("MiscellaneousTextEntries");
            
            // delete the Plugins table..
            Delete.Table("Plugins");

            // delete the RecentFiles table..
            Delete.Index("IX_RecentFile_Session_Id");
            Delete.Table("RecentFiles");
         
            // delete the SearchAndReplaceHistories table..
            Delete.Index("IX_SearchAndReplaceHistory_Session_Id");
            Delete.Table("SearchAndReplaceHistories");

            // delete the Plugins table..
            Delete.Table("SoftwareLicenses");
        }

        /// <summary>
        /// The simple text lines replace script (C#).
        /// </summary>
        public string SimpleReplaceScript { get; } =
            @"#region Usings
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion


public class ManipulateLineLines
{
    public static string Evaluate(List<string> fileLines)
    {
        string result = string.Empty;
        // replace text from the file''s lines..
        for (int i = 0; i < fileLines.Count; i++)
        {
           fileLines[i] = fileLines[i].Replace(""value1"", ""value2"");
        }

        result = string.Concat(fileLines); // concatenate the result lines.. 
        return result;
    }
}";

        /// <summary>
        /// The simple line ending change script (C#).
        /// </summary>
        public string SimpleLineEndingChangeScript { get; } =
            @"#region Usings
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion


public class ManipulateText
{
    public static string Evaluate(string fileContents)
    {
        // convert Windows line endings to Linux / Unix line endings..
        fileContents = fileContents.Replace(""\r\n"", ""\n""); 
        return fileContents;
    }
}";

        /// <summary>
        /// The simple XML manipulation script (C#).
        /// </summary>
        public string SimpleXmlManipulationScript { get; } =
            @"#region Usings
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
#endregion


public class ManipulateText
{
    public static string Evaluate(string fileContents)
    {
	    XDocument xDoc = XDocument.Parse(fileContents);

		// An example loop: foreach (XElement element in xDoc.Descendants(""give_a_valid_name""))
        // An example loop: {
			// Do something with the: element.Attribute(""some_name"").Value;
        // An example loop: }

        return xDoc.ToString();
    }
}";

        /// <summary>
        /// The license of this software.
        /// </summary>
        private string License { get; } =
            $@"MIT License

Copyright(c) {DateTime.Now.Year} Petteri Kautonen

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

        private const string SPDX_ID = @"MIT";
    }
}
