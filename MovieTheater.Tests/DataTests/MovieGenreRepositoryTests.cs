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
    internal class MovieGenreRepositoryTests
    {
        [Test]
        public async Task MovieGenreRepository_GetAsync_ReturnsAllEntities()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieGenreRepository(context);
            var expectedMovieGenres = ExpectedMovieGenres;

            // Act
            var movieGenres = await repository.GetAsync();

            // Assert
            Assert.That(movieGenres, Is.EqualTo(expectedMovieGenres).Using(new MovieGenreEqualityComparer()), message: "Method GetAsync works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task MovieGenreRepository_GetByIdAsync_ReturnsSingleEntity(int id)
        {
            // Arrange 
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieGenreRepository(context);
            var expectedMovieGenre = ExpectedMovieGenres.FirstOrDefault(mg => mg.Id == id);

            // Act
            var movieGenre = await repository.GetByIdAsync(id);

            // Assert
            Assert.That(movieGenre, Is.EqualTo(expectedMovieGenre).Using(new MovieGenreEqualityComparer()), message: "Method GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task MovieGenreRepository_InsertAsync_AddsEntityToDatabase()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieGenreRepository(context);
            var movieGenre = new MovieGenre { Id = 5, Name = "Horror" };

            // Act
            await repository.InsertAsync(movieGenre);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.MovieGenre.Count(), Is.EqualTo(5), message: "Method InsertAsync works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task MovieGenreRepository_DeleteByIdAsync_RemovesEntityFromDatabase(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new MovieGenreRepository(context);

            // Act
            await repository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.MovieGenre.Count(), Is.EqualTo(3), message: "Method DeleteByIdAsync works incorrectly");
        }

        private static IEnumerable<MovieGenre> ExpectedMovieGenres => 
            new[] 
            {
                new MovieGenre { Id = 1, Name = "Adventure"},
                new MovieGenre { Id = 2, Name = "Thriller" },
                new MovieGenre { Id = 3, Name = "Comedy" },
                new MovieGenre { Id = 4, Name = "Drama" }
            };
    }
}
