using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLiteWithEF
{
    public class RecordQueryTempDbContext : DbContext
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "StandardDataApp_HistoryRecordTemp.db");
        private static string StrConnection = $"Data Source={dbPath};";// Cache=Shared";
        public RecordQueryTempDbContext() :
            base(new SQLiteConnection(StrConnection), false)
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecordQueryTemp>().ToTable("RecordQueryTemp");
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            Database.SetInitializer(new SqliteContextInitializer<RecordQueryTempDbContext>(dbPath, modelBuilder));


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<RecordQueryTemp> RecordQueryTemp { get; set; }
    }
}
