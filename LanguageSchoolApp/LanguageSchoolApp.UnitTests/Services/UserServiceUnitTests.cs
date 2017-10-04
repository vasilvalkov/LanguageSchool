using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Data.Repositories;
using LanguageSchoolApp.Data.SaveContext;
using LanguageSchoolApp.Services;
using Moq;
using NUnit.Framework;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LanguageSchoolApp.UnitTests.Services
{
    [TestFixture]
    public class UserServiceUnitTests
    {
        [Test]
        public void UserIdByUsername_ShouldSearchAmongAllNotDeletedUsersInDb()
        {
            // Arrange
            string username = "user";
            User userStub = new User { UserName = username };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            courseService.UserIdByUsername(username);

            // Assert
            efRepositoryMock.Verify(repo => repo.AllNotDeleted, Times.Once);
        }

        [Test]
        public void UserIdByUsername_ShouldReturnTheCorrectUserIdByGivenUsernameInParameter()
        {
            // Arrange
            string username = "user";
            string userId = "12345678";
            User userStub = new User { UserName = username, Id = userId };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            var result = courseService.UserIdByUsername(username);

            // Assert
            Assert.AreEqual(userId, result);
        }

        [Test]
        public void ByUsername_ShouldSearchAmongAllNotDeletedUsersInDb()
        {
            // Arrange
            string username = "user";
            User userStub = new User { UserName = username };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            courseService.ByUsername(username);

            // Assert
            efRepositoryMock.Verify(repo => repo.AllNotDeleted, Times.Once);
        }

        [Test]
        public void ByUsername_ShouldReturnIQueryableWithUserWithGivenUsername_WhenUserWithSameUsernameExistsInDb()
        {
            // Arrange
            string username = "user";
            User userStub = new User { UserName = username };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            var result = courseService.ByUsername(username);

            // Assert
            Assert.AreEqual(username, result.FirstOrDefault().UserName);
        }

        [Test]
        public void ByUsername_ShouldReturnIQueryableWithNull_WhenUserWithSameUsernameDoesNotExistInDb()
        {
            // Arrange
            string username = "nonexisting user";
            User userStub = new User { UserName = "existing user" };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            var result = courseService.ByUsername(username);

            // Assert
            Assert.IsNull(result.FirstOrDefault());
        }

        [Test]
        public void GetCourses_ShouldSearchAmongAllNotDeletedUsersInDb()
        {
            // Arrange
            string username = "user";
            User userStub = new User { UserName = username, Courses = new List<Course>() };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            courseService.GetCourses(username);

            // Assert
            efRepositoryMock.Verify(repo => repo.AllNotDeleted, Times.Once);
        }

        [Test]
        public void GetCourses_ShouldReturnIQueryableWithCoursesOfUserWithGivenUsername_WhenUserWithSameUsernameExistsInDb()
        {
            // Arrange
            string username = "user";
            var userCourses = new List<Course>()
            {
                new Course() { Id = Guid.NewGuid() },
                new Course() { Id = Guid.NewGuid() }
            };

            User userStub = new User { UserName = username, Courses = userCourses };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            var result = courseService.GetCourses(username);

            // Assert
            CollectionAssert.AreEqual(userCourses, result.ToList());
        }

        [Test]
        public void EnrollInCourse_ShouldSearchAmongAllNotDeletedUsersInDb()
        {
            // Arrange
            string username = "user";
            var courseStub = new Course();
            User userStub = new User { UserName = username, Courses = new List<Course>() };

            var efRepositoryMock = new Mock<IEfRepository<User>>();
            efRepositoryMock
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextStub = new Mock<ISaveContext>();
            contextStub.Setup(c => c.Commit());

            var courseService = new UserService(efRepositoryMock.Object, contextStub.Object);

            // Act
            courseService.EnrollInCourse(username, courseStub);

            // Assert
            efRepositoryMock.Verify(repo => repo.AllNotDeleted, Times.Once);
        }

        [Test]
        public void EnrollInCourse_ShouldCallCommitMethodInContext()
        {
            // Arrange
            string username = "user";
            var courseStub = new Course();
            User userStub = new User { UserName = username, Courses = new List<Course>() };

            var efRepositoryStub = new Mock<IEfRepository<User>>();
            efRepositoryStub
                .Setup(repo => repo.AllNotDeleted)
                .Returns(new List<User>() { userStub }.AsQueryable());

            var contextMock = new Mock<ISaveContext>();
            contextMock.Setup(c => c.Commit());

            var courseService = new UserService(efRepositoryStub.Object, contextMock.Object);

            // Act
            courseService.EnrollInCourse(username, courseStub);

            // Assert
            contextMock.Verify(c => c.Commit(), Times.Once);
        }
    }
}
