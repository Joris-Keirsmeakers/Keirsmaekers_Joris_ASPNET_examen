using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keirsmaekers_joris_examen.Entities;
using Microsoft.EntityFrameworkCore;

namespace Keirsmaekers_joris_examen.Data
{
    public class EntityContext : DbContext
    {

        public EntityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(c => c.Id);
            modelBuilder.Entity<Car>().HasOne(c => c.Owner);
            modelBuilder.Entity<Car>().HasOne(c => c.Cartype);

            modelBuilder.Entity<Owner>().HasKey(o => o.Id);

            modelBuilder.Entity<Cartype>().HasKey(ct => ct.Id);
    

        }


        public DbSet<Car> Car { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Cartype> Cartype { get; set; }
    }
}
