using HastaneOtomasyonu.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HastaneOtomasyonu.Database
{
    public class HastanaDbContext:DbContext
    {
        public HastanaDbContext(DbContextOptions<HastanaDbContext> options): base(options)
        {

        }

        public DbSet<Ulke> Ulke { get; set; }
        public DbSet<Sehir> Sehir { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Hastane> Hastane { get; set; }
        public DbSet<Klinik> Klinik { get; set; }
        public DbSet<HastaneKlinik> HastaneKlinik { get; set; }
        public DbSet<Kisi> Kisi { get; set; }
        public DbSet<KanGrubu> KanGrubu { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Hasta> Hasta { get; set; }
        public DbSet<Doktor> Doktor { get; set; }
        public DbSet<Randevu> Randevu { get; set; }
        public DbSet<Muayene> Muayene { get; set; }
        public DbSet<Recete> Recete { get; set; }
        public DbSet<Ilac> Ilac { get; set; }
        public DbSet<ReceteIlac> ReceteIlac { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<MuayeneTest> MuayeneTest { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<Recete>().HasKey(x => x.receteNo);
            modelBuilder.Entity<Ilac>().HasKey(x => x.ilacKodu);

        }
    }
}
