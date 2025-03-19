
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GD.RequestSystem.Entities;
using GD.RequestSystem.Entities.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GD.RequestSystem.DAL.EF
{
    public class RequestSystemDbContext : DbContext
    {
        public RequestSystemDbContext() { }
        private readonly IConfiguration _configuration;
        public RequestSystemDbContext(IConfiguration configuration)
        {
          this._configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("dbRequestSystem"));
        }
        public DbSet<Petition> Petitions { get; set; }
        public DbSet<ResultPetition> ResultPetition { get; set; }
        public DbSet<PetitionStatus> PetitionStatus { get; set; }
        public DbSet<Area> Areas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>().ToTable("tblAreas");
            modelBuilder.Entity<Petition>().ToTable("tblPetition");
            modelBuilder.Entity<ResultPetition>().ToTable("tblResultPetition");
            modelBuilder.Entity<PetitionStatus>().ToTable("tblPetitionStatus");


        }

    }
}
