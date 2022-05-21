using Core.Models;
using DataAccess.Contexts;
using DataAccess.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTheater.Tests.DataTests
{
    [TestFixture]
    internal class MovieRepositoryTests
    {
        [Test]
        public async Task MovieRepository_GetAsync_ReturnsAllEntities()
        {
            // Arrange 
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieRepository(context);
            var expectedMovies = ExpectedMovies;

            // Act
            var movies = await repository.GetAsync();

            // Assert
            Assert.That(movies, Is.EqualTo(expectedMovies).Using(new MovieEqualityComparer()), message: "Method GetAsync works incorrectly");
        }

        [Test]
        public async Task MovieRepository_GetPagedMovies_ReturnsAllEntities()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieRepository(context);
            var expectedMovies = ExpectedMovies;

            // Act
            var movies = await repository.GetPagedMovies();

            // Assert
            Assert.That(movies, Is.EqualTo(expectedMovies).Using(new MovieEqualityComparer()), message: "Method GetPagedMovies works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task MovieRepository_GetByIdAsync_ReturnsSingleEntity(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieRepository(context);
            var expectedMovie = ExpectedMovies.FirstOrDefault(m => m.Id == id);

            // Act
            var movie = await repository.GetByIdAsync(id);

            // Assert
            Assert.That(movie, Is.EqualTo(expectedMovie).Using(new MovieEqualityComparer()), message: "Method GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task MovieRepository_InsertAsync_AddsEntityToDatabase()
        {
            // Arrange 
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieRepository(context);
            var movie = new Movie { Id = 5, Title = "FifthMovie", ReleaseDate = 2012, Director = "Director", Description = "Long description", TrailerUrl = "someUrl" };

            // Act
            await repository.InsertAsync(movie);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.Movies.Count(), Is.EqualTo(5), message: "Method InsertAsync works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task MovieRepository_DeleteByIdAsync_RemovesEntityFromDatabase(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieRepository(context);

            // Act
            await repository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.Movies.Count(), Is.EqualTo(3), message: "Method DeleteByIdAsync works incorrectly");
        }

        private static IEnumerable<Movie> ExpectedMovies =>
            new[]
            {
                new Movie { Id = 1, Title = "FirstMovie", ReleaseDate = 2022, Director = "Popular Director", Description = "Pretty long description", TrailerUrl = "someUrl" },
                new Movie { Id = 2, Title = "SecondMovie", ReleaseDate = 2020, Director = "John Smith", Description = "Once upon a time...", TrailerUrl = "someUrl" },
                new Movie { Id = 3, Title = "ThirdMovie", ReleaseDate = 2021, Director = "Elon Musk", Description = "Tesla, SpaceX and a lot more...", TrailerUrl = "someUrl" },
                new Movie { Id = 4, Title = "FourthMovie", ReleaseDate = 2000, Director = "Sisters Vachowski", Description = "Keanu Reeves is a badasss...", TrailerUrl = "someUrl" }
            };
    }
}
