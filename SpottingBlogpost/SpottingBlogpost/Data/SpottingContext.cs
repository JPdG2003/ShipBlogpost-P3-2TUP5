using Microsoft.EntityFrameworkCore;
using SpottingBlogpost.Data.Entities;
using System.Reflection.Emit;

namespace SpottingBlogpost.Data
{
    public class SpottingContext : DbContext 
    {

        public SpottingContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Spotter> Spotters { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);
            base.OnModelCreating(modelBuilder);
        }

    }
}
