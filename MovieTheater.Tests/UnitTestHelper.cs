using Core.Models;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<AppDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new AppDbContext(options))
            {
                SeedData(context);
            }

            return options;
            //return new DbContextOptionsBuilder<AppDbContext>().Options();
        }

        public static void SeedData(AppDbContext context)
        {
            context.Movies.AddRange(
                new Movie { Id = 1, Title = "FirstMovie", ReleaseDate = 2022, Director = "Popular Director", Description = "Pretty long description", TrailerUrl = "someUrl" },
                new Movie { Id = 2, Title = "SecondMovie", ReleaseDate = 2020, Director = "John Smith", Description = "Once upon a time...", TrailerUrl = "someUrl" },
                new Movie { Id = 3, Title = "ThirdMovie", ReleaseDate = 2021, Director = "Elon Musk", Description = "Tesla, SpaceX and a lot more...", TrailerUrl = "someUrl" },
                new Movie { Id = 4, Title = "FourthMovie", ReleaseDate = 2000, Director = "Sisters Vachowski", Description = "Keanu Reeves is a badasss...", TrailerUrl = "someUrl" });

            context.Comments.AddRange(
                new Comment { Id = 1, MovieId = 1, Text = "Great movie", UserId = "1", UserName = "Kostia" },
                new Comment { Id = 2, MovieId = 1, Text = "Loved it!", UserId = "2", UserName = "Anonymous" },
                new Comment { Id = 3, MovieId = 2, Text = "This is fire", UserId = "1", UserName = "Kostia" });

            context.MovieGenre.AddRange(
                new MovieGenre { Id = 1, Name = "Adventure"},
                new MovieGenre { Id = 2, Name = "Thriller" },
                new MovieGenre { Id = 3, Name = "Comedy" },
                new MovieGenre { Id = 4, Name = "Drama" });

            context.Users.AddRange(
                new User { Id = "1", Age = 18, Email = "email1@gmail.com", UserName = "First User", Login = "User1" },
                new User { Id = "2", Age = 20, Email = "email2@gmail.com", UserName = "Second User", Login = "User2" },
                new User { Id = "3", Age = 41, Email = "email3@gmail.com", UserName = "Third User", Login = "User3" },
                new User { Id = "4", Age = 14, Email = "email4@gmail.com", UserName = "Fourth User", Login = "User4" });

            context.SaveChanges();
            
        }
    }
}
