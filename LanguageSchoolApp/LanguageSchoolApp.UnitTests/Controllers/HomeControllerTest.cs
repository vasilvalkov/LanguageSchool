using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using LanguageSchoolApp;
using LanguageSchoolApp.Controllers;
using NUnit.Framework;
using Moq;
using LanguageSchoolApp.Services.Contracts;

namespace LanguageSchoolApp.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();
            HomeController controller = new HomeController(courseServiceStub.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void About()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();
            HomeController controller = new HomeController(courseServiceStub.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();
            HomeController controller = new HomeController(courseServiceStub.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
