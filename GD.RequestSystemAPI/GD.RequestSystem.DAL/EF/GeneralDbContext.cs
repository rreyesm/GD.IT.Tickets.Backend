using GD.QualityAssurance.Entities.ModelsAdmin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GD.QualityAssurance.DAL.EF
{
    public class GeneralDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public GeneralDbContext() { }

        public GeneralDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("dbGeneral"));
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Permission> Permissions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("tblAccounts");
            modelBuilder.Entity<Department>().ToTable("tblDepartments");
            modelBuilder.Entity<User>().ToTable("tblUsers");
            modelBuilder.Entity<Project>().ToTable("tblProjects");
            modelBuilder.Entity<Module>().ToTable("tblModules");
            modelBuilder.Entity<Profile>().ToTable("tblProfiles");
            modelBuilder.Entity<Permission>().ToTable("tblPermissions");
        }
    }
}
