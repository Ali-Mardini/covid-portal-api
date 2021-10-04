using covid_portal_api.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace covid_portal_api.infrastructure.Data
{
    public class CovidContext : DbContext
    {
        public CovidContext(DbContextOptions<CovidContext> options) : base(options)
        {
        }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Cases)
                .WithOne(e => e.Country)
                .HasForeignKey(e => e.CountryId);
        }
    }
}
