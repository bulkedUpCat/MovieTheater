using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
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

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(m => m.Movies)
                .UsingEntity(t => t.ToTable("MovieGenres"));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
    }
}
