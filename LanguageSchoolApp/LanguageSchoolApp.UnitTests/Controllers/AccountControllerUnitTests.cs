using LanguageSchoolApp.Controllers;
using NUnit.Framework;
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
