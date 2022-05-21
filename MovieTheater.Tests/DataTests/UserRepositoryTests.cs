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
    internal class UserRepositoryTests
    {
        [Test]
        public async Task UserRepository_GetAsync_ReturnsAllEntities()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new UserRepository(context);
            var expectedUsers = ExpectedUsers;

            // Act
            var users = await repository.GetAsync();

            // Assert
            Assert.That(users, Is.EqualTo(expectedUsers).Using(new UserEqualityComparer()), message: "Method GetAsync works incorrectly");
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        public async Task UserRepository_GetByIdAsync_ReturnsSingleEntity(string id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new UserRepository(context);
            var expectedUser = ExpectedUsers.FirstOrDefault(u => u.Id == id);

            // Act
            var user = await repository.GetByIdAsync(id);

            // Assert
            Assert.That(user, Is.EqualTo(expectedUser).Using(new UserEqualityComparer()), message: "Method GetByIdAsync works incorrectly");
        }

        [Test]
        public async Task UserRepository_InsertAsync_AddsEntityToDatabase()
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new UserRepository(context);
            var user = new User { Id = "5", Age = 18, Email = "email5@gmail.com", UserName = "Fifth User", Login = "User5" };

            // Act
            await repository.InsertAsync(user);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.Users.Count(), Is.EqualTo(5), message: "Method InsertAsync works incorrectly");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task UserRepository_DeleteByIdAsync_RemovesEntityFromDatabase(int id)
        {
            // Arrange
            using var context = new AppDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new UserRepository(context);

            // Act
            await repository.DeleteByIdAsync(id);
            await context.SaveChangesAsync();

            // Assert
            Assert.That(context.Users.Count(), Is.EqualTo(3), message: "Method DeleteByIdAsync works incorrectly");
        }

        private static IEnumerable<User> ExpectedUsers =>
            new[]
            {
                new User { Id = "1", Age = 18, Email = "email1@gmail.com", UserName = "First User", Login = "User1"},
                new User { Id = "2", Age = 20, Email = "email2@gmail.com", UserName = "Second User", Login = "User2" },
                new User { Id = "3", Age = 41, Email = "email3@gmail.com", UserName = "Third User", Login = "User3" },
                new User { Id = "4", Age = 14, Email = "email4@gmail.com", UserName = "Fourth User", Login = "User4" }
            };
    }
}
