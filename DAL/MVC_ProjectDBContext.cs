using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using MVC_Project.DAL;
using System.IO;
using System.Runtime.InteropServices;

namespace MVC_Project.DAL
{
    public class MVC_ProjectDBContext : DbContext
    {
        /* DbContexts contain all the tables that should be in a DB */

        // Tables
        public DbSet<Expense> Expenses { get; set; }

        // Database provider
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            GrabLocalDatabaseCredentials grabCredentials = new GrabLocalDatabaseCredentials();
            Dictionary<string, string> creds = grabCredentials.OpenLocalAuthFile();

            string connectionString = creds["SQLServer_Win"];
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                connectionString = creds["SQLServer_Mac"];
            }

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
