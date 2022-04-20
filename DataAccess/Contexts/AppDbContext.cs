using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.WatchLaterMovies)
                .WithMany(m => m.WatchLaterUsers)
                .UsingEntity(t => t.ToTable("WatchLater"));

            modelBuilder.Entity<User>()
                .HasMany(u => u.FavoriteListMovies)
                .WithMany(m => m.FavoriteListUsers)
                .UsingEntity(t => t.ToTable("FavoriteList"));
        }

        DbSet<User> Users { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Comment> Comments { get; set; }
    }
}
