using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using pz_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz_2.Context
{
    internal class BusinessContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }


        public BusinessContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;" +
                "Database=pz_2;" +
                "Trusted_Connection=True;");
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(
        //        new User[]
        //        {
        //            new User {Id=1, Name="Tom", Age=23},
        //            new User {Id=2, Name="Alice", Age=26 },
        //            new User {Id=3, Name="Bob", Age=20}
        //        });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
