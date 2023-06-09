﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Surveys> Surveys { get; set; }
        public DbSet<SurveyAnswers> SurveyAnswers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "./Data/AppDB.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
