using LanguageSchoolApp.Data;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Data.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LanguageSchoolApp.UnitTests.Data.Repositories
{
    [TestFixture]
    public class EfRepositoryUnitTests
    {
        private static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));

            return dbSet.Object;
        }

        [Test]
        public void AllNotDeleted_ShouldReturnIQueryableOfEntitiesWithIsDeletedFlagSetToFalse()
        {
            // Arrange
            Course sampleNotDeletedCourse = new Course { IsDeleted = false };
            Course sampleDeletedCourse = new Course { IsDeleted = true };
            var courses = new List<Course>()
            {
                sampleNotDeletedCourse,
                sampleDeletedCourse
            };

            var coursesDbSetStub = GetQueryableMockDbSet(courses);

            var contextStub = new Mock<MsSqlDbContext>();
            contextStub.Setup(c => c.Set<Course>()).Returns(coursesDbSetStub);

            var repository = new EfRepository<Course>(contextStub.Object);

            // Act
            var result = repository.AllNotDeleted;

            // Assert
            Assert.AreEqual(1, result.ToList().Count);
        }

        [Test]
        public void All_ShouldReturnIQueryableOfAllExistingEntitiesRegardlessOfIsDeletedFlagSetting()
        {
            // Arrange
            Course sampleNotDeletedCourse = new Course { IsDeleted = false };
            Course sampleDeletedCourse = new Course { IsDeleted = true };
            var courses = new List<Course>()
            {
                sampleNotDeletedCourse,
                sampleDeletedCourse
            };

            var coursesDbSetStub = GetQueryableMockDbSet(courses);

            var contextStub = new Mock<MsSqlDbContext>();
            contextStub.Setup(c => c.Set<Course>()).Returns(coursesDbSetStub);

            var repository = new EfRepository<Course>(contextStub.Object);

            // Act
            var result = repository.All;

            // Assert
            Assert.AreEqual(2, result.ToList().Count);
        }
    }
}
