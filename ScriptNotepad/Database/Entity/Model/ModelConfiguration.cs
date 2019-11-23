using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptNotepad.Database.Entity.Entities;

namespace ScriptNotepad.Database.Entity.Model
{
    public class ModelConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder)
        {
            //ConfigureSimpleEntity<FileSave>(modelBuilder);
            ConfigureFileSaveEntity(modelBuilder);
        }

        private static void ConfigureSimpleEntity<T>(DbModelBuilder modelBuilder) where T : class
        {
            modelBuilder.Entity<T>();
        }

        private static void ConfigureFileSaveEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileSave>();
        }
    }
}
