using LanguageSchoolApp.Data;
using LanguageSchoolApp.Data.SaveContext;
using Moq;
using NUnit.Framework;

namespace LanguageSchoolApp.UnitTests.Data
{
    [TestFixture]
    public class SaveContextUnitTests
    {
        [Test]
        public void Commit_ShouldCallSaveChangesToDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<MsSqlDbContext>();
            dbContextMock.Setup(dbc => dbc.SaveChanges());

            var saveContext = new SaveContext(dbContextMock.Object);
            // Act
            saveContext.Commit();

            // Assert
            dbContextMock.Verify(dbc => dbc.SaveChanges(), Times.Once);
        }
    }
}
