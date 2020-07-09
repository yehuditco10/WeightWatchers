using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tracking.Data
{
    public class TrackingContext : DbContext
    {
        public DbSet<Entities.Tracking> Trackings { get; set; }
       
        public TrackingContext(DbContextOptions<TrackingContext> options)
       : base(options)
        { }
        public TrackingContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Entities.Tracking>().ToTable("Tracking");
            modelBuilder.Entity<Entities.Tracking>()
                               .Property(m => m.Id);
            modelBuilder.Entity<Entities.Tracking>()
                                  .Property(m => m.CardId);
            modelBuilder.Entity<Entities.Tracking>()
               .Property(m => m.Date)
               .HasDefaultValueSql("getDate()");
            modelBuilder.Entity<Entities.Tracking>()
                .Property(u => u.Trand);
            modelBuilder.Entity<Entities.Tracking>()
               .Property(u => u.Weight);
            modelBuilder.Entity<Entities.Tracking>()
                .Property(u => u.BMI)
                 .HasDefaultValue(0);
            modelBuilder.Entity<Entities.Tracking>()
               .Property(u => u.Comments);
        }
    }
}
