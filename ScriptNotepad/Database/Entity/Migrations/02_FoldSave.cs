using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace ScriptNotepad.Database.Entity.Migrations
{
    /// <summary>
    /// Add a FoldSave column to the FileSave table.
    /// Implements the <see cref="FluentMigrator.Migration" />
    /// </summary>
    /// <seealso cref="FluentMigrator.Migration" />
    [Migration(2021_0325_17_17_49)]    
    public class FoldSave: Migration
    {
        /// <summary>
        /// Collect the UP migration expressions
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Up()
        {
            this.Alter.Table("FileSaves")
                .AddColumn("FoldSave").AsString().WithDefaultValue(string.Empty);
        }

        /// <summary>
        /// Collects the DOWN migration expressions
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Down()
        {
            Delete.Column("FoldSave").FromTable("FileSaves");
        }
    }
}
