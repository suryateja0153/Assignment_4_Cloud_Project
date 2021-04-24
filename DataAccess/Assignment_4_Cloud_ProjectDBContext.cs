using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment_4_Cloud_Project.Models;
//using DbContext = Microsoft.EntityFrameworkCore.DbContext;
using Microsoft.EntityFrameworkCore.SqlServer;
using Assignment_4_Cloud_Project.APIManager;

namespace Assignment_4_Cloud_Project.DataAccess
{
    public class Assignment_4_Cloud_ProjectDBContext : DbContext
    {
        public Assignment_4_Cloud_ProjectDBContext(DbContextOptions<Assignment_4_Cloud_ProjectDBContext> options) : base(options)
        {
        }

        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Locale> Locales { get; set; }
    }
}