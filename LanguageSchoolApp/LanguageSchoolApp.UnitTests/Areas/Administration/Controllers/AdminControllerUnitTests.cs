using LanguageSchoolApp.Areas.Administration.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Areas.Administration.Controllers
{
    [TestFixture]
    public class AdminControllerUnitTests
    {
        [Test]
        public void Index_ShouldReturnNonNullResult()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public void Index_ShouldReturnDefaultView()
        {
            // Arrange
            AdminController controller = new AdminController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
