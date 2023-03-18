
using HTS.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HTS.Data
{
    [ConnectionStringName("Default")]
    public class AppDbContext : AbpDbContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
