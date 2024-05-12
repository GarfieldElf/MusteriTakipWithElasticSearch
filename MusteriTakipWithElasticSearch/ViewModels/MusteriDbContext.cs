using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MusteriTakipWithElasticSearch.Models;

namespace MusteriTakipWithElasticSearch.ViewModels
{
    public class MusteriDbContext : DbContext
    {
        public MusteriDbContext() { }
        public MusteriDbContext(DbContextOptions<MusteriDbContext> options) : base(options) { }
        public DbSet<Musteri> Musteriler { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-OHVI9IG;Initial Catalog=Test;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musteri>()
                .Property(b => b.MusteriTel)
               .HasDefaultValueSql();  // get sql default value (belirtilmedi)

            modelBuilder.Entity<Musteri>()
                .Property(b => b.MusteriEposta)
                .HasDefaultValueSql();

        }

    }
}
