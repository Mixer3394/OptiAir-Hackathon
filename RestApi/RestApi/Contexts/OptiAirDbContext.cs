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
            optionsBuilder.UseSqlite("Filename=OptiAirDatabse.db"/*, x => x.SuppressForeignKeyEnforcement()*/);
            //UseSqlite($"Filename={databaseName}", x => x.SuppressForeignKeyEnforcement());
        }

        public DbSet<Device> Devices
        {
            get;
            set;
        }
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Measurement>()
            //    .HasOne(m => m.Device)
            //    .WithMany(d => d.Measurements)
            //    .HasForeignKey(m => m.MAC);

            modelBuilder.Entity<Measurement>()
                .HasOne<Device>()
                .WithMany()
                .HasForeignKey(m => m.MAC);

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<Measurement>()
        //    // .HasOne(p => p.Device)
        //    // .WithMany(b => b.Measurements)
        //    // .HasForeignKey(p => p.DeviceMAC);

        //    modelBuilder.Entity<Device>()



        //    //    modelBuilder.Entity<Measurement>()
        //    //         .HasOne(relation => relation.Device)
        //    //         .WithMany(issue => issue.Measurements)
        //    //         .HasForeignKey(relation => relation.DeviceMAC);
        //    //    //modelBuilder.Entity<IssueSprint>()
        //    //    //    .HasKey(record => new { record.IssueId, record.SprintId });

        //    //    //// 1:N connection from relation entity to issue
        //    //    //modelBuilder.Entity<IssueSprint>()
        //    //    //    .HasOne(relation => relation.Issue)
        //    //    //    .WithMany(issue => issue.Sprints)
        //    //    //    .HasForeignKey(relation => relation.IssueId);

        //    //    //// 1:N connection from relation entity to sprint
        //    //    //modelBuilder.Entity<IssueSprint>()
        //    //    //    .HasOne(relation => relation.Sprint)
        //    //    //    .WithMany(sprint => sprint.Issues)
        //    //    //    .HasForeignKey(relation => relation.SprintId);

        //    //    base.OnModelCreating(modelBuilder);
        //}


    }
}
