using LanguageSchoolApp.Controllers;
using LanguageSchoolApp.Services.Contracts;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerUnitTests
    {
        [Test]
        public void Login_ShouldStoreThePassedReturnUrlInViewBag()
        {
            // Arrange
            var returnUrl = "return/url";
            var controller = new AccountController();

            // Act
            var result = controller.Login(returnUrl) as ViewResult;

            // Assert
            Assert.AreEqual(returnUrl, result.ViewBag.ReturnUrl);
        }

        [Test]
        public void ConfirmEmail_ShouldCallConfirmEmailMethodInUserManagementService()
        {
            // Arrange
            string validUserId = "1234";
            string validCode = "123";

            var userManagerStub = new Mock<IUserManagementService>();
            userManagerStub
                .Setup(um => um.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()));

            var signInManagerMock = new Mock<ISignInService>();

            var controller = new AccountController(userManagerStub.Object, signInManagerMock.Object);

            // Act
            var result = controller.ConfirmEmail(validUserId, validCode);

            // Assert
            userManagerStub.Verify(um => um.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ConfirmEmail_ShouldReturnViewWithNameError_WhenConfirmationDoesNotSucceed()
        {
            // Arrange
            string validUserId = "1234";
            string validCode = "123";

            var userManagerStub = new Mock<IUserManagementService>();
            userManagerStub
                .Setup(um => um.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(Mock.Of<IdentityResult>());

            var signInManagerMock = new Mock<ISignInService>();

            var controller = new AccountController(userManagerStub.Object, signInManagerMock.Object);

            // Act
            var result = controller.ConfirmEmail(validUserId, validCode);

            // Assert
            Assert.AreEqual("Error", (result.Result as ViewResult).ViewName);
        }

        [Test]
        public void ConfirmEmail_ShouldReturnViewWithNameError_WhenPassedUserIdIsNull()
        {
            // Arrange
            string validCode = "123";
            var userManagerStub = new Mock<IUserManagementService>();
            var signInManagerMock = new Mock<ISignInService>();

            var controller = new AccountController(userManagerStub.Object, signInManagerMock.Object);

            // Act
            var result = controller.ConfirmEmail(null, validCode);

            // Assert
            Assert.AreEqual("Error", (result.Result as ViewResult).ViewName);
        }

        [Test]
        public void ConfirmEmail_ShouldReturnViewWithNameError_WhenPassedCodeIsNull()
        {
            // Arrange
            string validUserId = "1234";
            var userManagerStub = new Mock<IUserManagementService>();
            var signInManagerMock = new Mock<ISignInService>();

            var controller = new AccountController(userManagerStub.Object, signInManagerMock.Object);

            // Act
            var result = controller.ConfirmEmail(validUserId, null);

            // Assert
            Assert.AreEqual("Error", (result.Result as ViewResult).ViewName);
        }

        [Test]
        public void Register_ShouldReturnDefaultView()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ForgotPassword_ShouldReturnDefaultView()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPassword() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ForgotPasswordConfirmation_ShouldReturnDefaultView()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ForgotPasswordConfirmation() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ResetPasswordHttpGet_ShouldReturnViewWithNameError_WhenPassedParameterIsNull()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPassword((string)null) as ViewResult;

            // Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [Test]
        public void ResetPasswordHttpGet_ShouldReturnDefaultView_WhenPassedParameterIsValid()
        {
            // Arrange
            var controller = new AccountController();
            string validParameter = "reset code";

            // Act
            var result = controller.ResetPassword(validParameter) as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ResetPasswordConfirmation_ShouldReturnDefaultView()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ResetPasswordConfirmation() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [Test]
        public void ExternalLoginFailure_ShouldReturnDefaultView()
        {
            // Arrange
            var controller = new AccountController();

            // Act
            var result = controller.ExternalLoginFailure() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
