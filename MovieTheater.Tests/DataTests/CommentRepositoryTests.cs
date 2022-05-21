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
    internal class CommentRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task CommentRepository_GetByMovieIdAsync_ReturnsCommentsOfSpecifiedMovie(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new CommentRepository(context);
            var expectedComments = ExpectedComments.Where(c => c.MovieId == id);

            // Act
            var comments = await repository.GetByMovieIdAsync(id);

            // Assert
            Assert.That(comments, Is.EqualTo(expectedComments).Using(new CommentEqualityComparer()), message: "Method GetByMovieIdAsync works incorrectly");
        }

        [Test]
        public async Task CommentRepository_GetAsync_ReturnsAllEntities()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new CommentRepository(context);
            var expectedComments = ExpectedComments;

            // Act
            var comments = await repository.GetAsync();

            // Assert
            Assert.That(comments, Is.EqualTo(expectedComments).Using(new CommentEqualityComparer()), message: "Method GetAsync works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CommentRepository_GetByIDAsync_ReturnsSingleEntity(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new CommentRepository(context);
            var expectedComment = ExpectedComments.FirstOrDefault(c => c.Id == id);

            // Act
            var comment = await repository.GetByIdAsync(id);

            // Assert
            Assert.That(comment, Is.EqualTo(expectedComment).Using(new CommentEqualityComparer()), message: "Method GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task CommentRepository_InsertAsync_AddsEntityToDatabase()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new CommentRepository(context);
            var comment = new Comment { Id = 4, MovieId = 1, Text = "Fantastic!", UserId = "1", UserName = "Kostia" };

            // Act
            await repository.InsertAsync(comment);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.Comments.Count, Is.EqualTo(4), message: "Method InsertAsync works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task CommentRepository_DeleteByIdAsync_RemovesEntityFromDatabase(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new CommentRepository(context);

            // Act
            await repository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.Comments.Count(), Is.EqualTo(2), message: "Method DeleteByIdAsync works incorrectly");
        }

        private static IEnumerable<Comment> ExpectedComments =>
            new[]
            {
                new Comment { Id = 1, MovieId = 1, Text = "Great movie", UserId = "1", UserName = "Kostia" },
                new Comment { Id = 2, MovieId = 1, Text = "Loved it!", UserId = "2", UserName = "Anonymous" },
                new Comment { Id = 3, MovieId = 2, Text = "This is fire", UserId = "1", UserName = "Kostia" },
            };
    }
}
