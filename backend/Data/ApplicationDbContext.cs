using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<LostItem> LostItems {get; set;}
        public DbSet<FoundItem> FoundItems {get; set;}
        public DbSet<Claim> Claims {get;set;}
        //Had to add this override otherwise migrations creates a non-FK FoundId column
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
            .HasOne(e => e.FoundItem)
            .WithMany(e => e.Claims)
            .HasForeignKey(e => e.FoundId)
            .IsRequired();
        }

    }
}