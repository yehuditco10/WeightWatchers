using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WeightWatchers.Data.Entities;
using WeightWatchers.Services.Models;
namespace WeightWatchers.Data
{
    public class WeightWatchersContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Card> Cards { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source =ILBHARTMANLT; Initial Catalog = weightWatchers; Integrated Security = True");
        //        base.OnConfiguring(optionsBuilder);
        //    }
        //}
        public WeightWatchersContext(DbContextOptions<WeightWatchersContext> options)
   : base(options)
        { }
        public WeightWatchersContext()
        {   }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Card>().ToTable("Cards");
        //    modelBuilder.Entity<Subscriber>().ToTable("Subscribers");

        //    modelBuilder.Entity<Card>()
        //                       .Property(Subscriber => Subscriber.openDate)
        //                           .HasDefaultValueSql("getdate()");
        //    modelBuilder.Entity<Card>()
        //                      .Property(Subscriber => Subscriber.updateDate)
        //                          .HasDefaultValueSql("getdate()");
        //    modelBuilder.Entity<Card>()
        //        .Property(u => u.weight)
        //        .HasDefaultValue(0);
        //    modelBuilder.Entity<Card>()
        //       .Property(u => u.BMI)
        //       .HasDefaultValue(0);
        //    modelBuilder.Entity<Card>()
        //        .Property(u => u.id);
        //     // .HasDefaultValueSql("NEWID()");
        //    modelBuilder.Entity<Subscriber>()
        //        .Property(u => u.id);
        //     //  .HasDefaultValueSql("NEWID()");
        //    modelBuilder.Entity<Subscriber>()

        //          .HasIndex(u => u.email)
        //          .IsUnique();


        //    modelBuilder.Entity<Subscriber>()
        //   .Property(u => u.firstName)
        //   .IsRequired();

        //    modelBuilder.Entity<Subscriber>()
        //   .Property(u => u.password)
        //   .IsRequired();
        //}
    }
}
