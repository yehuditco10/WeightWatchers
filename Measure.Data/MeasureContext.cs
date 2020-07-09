using System;
using Microsoft.EntityFrameworkCore;

namespace Measure.Data
{
    public class MeasureContext : DbContext
    {
        public DbSet<Entities.Measure> Measures { get; set; }
      
        public MeasureContext(DbContextOptions<MeasureContext> options)
       : base(options)
        { }
        public MeasureContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Entities.Measure>().ToTable("Measure");

            modelBuilder.Entity<Entities.Measure>()
                               .Property(m => m.id);
            modelBuilder.Entity<Entities.Measure>()
                                  .Property(m => m.weight)
                                  .HasDefaultValue(0);
            modelBuilder.Entity<Entities.Measure>()
               .Property(m => m.cardId);
            modelBuilder.Entity<Entities.Measure>()
                .Property(u => u.date)
                .HasDefaultValueSql("getDate()");
            modelBuilder.Entity<Entities.Measure>()
                .Property(u => u.status);
        }
    }
}

