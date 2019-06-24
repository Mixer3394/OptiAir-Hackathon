using Microsoft.EntityFrameworkCore;
using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Contexts
{
    public class OptiAirDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=OptiAirDatabse.db");
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
    }
}
