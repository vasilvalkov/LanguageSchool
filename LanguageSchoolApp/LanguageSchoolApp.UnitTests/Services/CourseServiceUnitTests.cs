using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Data.Repositories;
using LanguageSchoolApp.Data.SaveContext;
using LanguageSchoolApp.Services;
using Moq;
using NUnit.Framework;

namespace LanguageSchoolApp.UnitTests.Services
{
    [TestFixture]
    public class CourseServiceUnitTests
    {
        [Test]
        public void GetAll_ShouldReturnAllCoursesFromDbThatHaveTheIsDeletedFlagSetToFalse()
        {
            // Arrange
            var efRepositoryMock = new Mock<IEfRepository<Course>>();
            efRepositoryMock.Setup(repo => repo.AllNotDeleted);

            var contextStub = new Mock<ISaveContext>();
            var courseService = new CourseService(efRepositoryMock.Object, contextStub.Object);

            // Act
            var result = courseService.GetAll();

            // Assert
            efRepositoryMock.Verify(repo => repo.AllNotDeleted, Times.Once);
        }

        [Test]
        public void Update_ShouldCallUpdateMethodInCourseRepository()
        {
            // Arrange
            var efRepositoryMock = new Mock<IEfRepository<Course>>();
            efRepositoryMock.Setup(repo => repo.Update(It.IsAny<Course>()));

            var contextStub = new Mock<ISaveContext>();
            var courseService = new CourseService(efRepositoryMock.Object, contextStub.Object);
            Course course = new Course();

            // Act
            courseService.Update(course);

            // Assert
            efRepositoryMock.Verify(repo => repo.Update(It.IsAny<Course>()), Times.Once);
        }

        [Test]
        public void Update_ShouldCallCommitMethodInContext()
        {
            // Arrange
            var efRepositoryStub = new Mock<IEfRepository<Course>>();
            efRepositoryStub.Setup(repo => repo.Update(It.IsAny<Course>()));

            var contextMock = new Mock<ISaveContext>();
            contextMock.Setup(c => c.Commit());

            var courseService = new CourseService(efRepositoryStub.Object, contextMock.Object);
            Course course = new Course();

            // Act
            courseService.Update(course);

            // Assert
            contextMock.Verify(c => c.Commit(), Times.Once);
        }
    }
}
