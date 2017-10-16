using AutoMapper;
using AutoMapper.QueryableExtensions;
using LanguageSchoolApp.Controllers;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Models.Home;
using LanguageSchoolApp.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void Index_ShouldReturnNonNullResult()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseTileViewModel>();
                cfg.CreateMap<CourseTileViewModel, Course>();
            });
            var courseServiceStub = new Mock<ICourseService>();
            var mapperStub = new Mock<IMapper>();

            HomeController controller = new HomeController(courseServiceStub.Object, mapperStub.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Index_ViewModel_ShouldHaveExactlyThreeUpcomingCourses()
        {
            // Arrange
            var listOfCourses = new List<Course>()
            {
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)}
            };

            var courseServiceStub = new Mock<ICourseService>();
            courseServiceStub.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var mapperStub = new Mock<IMapper>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseTileViewModel>();
                cfg.CreateMap<CourseTileViewModel, Course>();
            });

            HomeController controller = new HomeController(courseServiceStub.Object, mapperStub.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(3, ((HomeViewModel)result.ViewData.Model).Courses.Count());
        }

        [Test]
        public void About()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();
            var mapperStub = new Mock<IMapper>();

            HomeController controller = new HomeController(courseServiceStub.Object, mapperStub.Object);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [Test]
        public void Contact_ShouldReturnNonNullResult()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();
            var mapperStub = new Mock<IMapper>();
            HomeController controller = new HomeController(courseServiceStub.Object, mapperStub.Object);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PageNotFound_ShouldReturnDefaultView()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();
            var mapperStub = new Mock<IMapper>();
            HomeController controller = new HomeController(courseServiceStub.Object, mapperStub.Object);

            // Act
            ViewResult result = controller.PageNotFound() as ViewResult;

            // Assert
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}
