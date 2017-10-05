using LanguageSchoolApp.Controllers;
using LanguageSchoolApp.Services.Contracts;
using Moq;
using NUnit.Framework;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Controllers
{
    [TestFixture]
    public class ManageControllerUnitTests
    {
        [Test]
        public void AddPhoneNumber_ShouldReturnDefaultView()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            var controller = new ManageController(userServiceStub.Object);

            // Act
            var result = controller.AddPhoneNumber() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ChangePassword_ShouldReturnDefaultView()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            var controller = new ManageController(userServiceStub.Object);

            // Act
            var result = controller.ChangePassword() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void SetPassword_ShouldReturnDefaultView()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();
            var controller = new ManageController(userServiceStub.Object);

            // Act
            var result = controller.SetPassword() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
