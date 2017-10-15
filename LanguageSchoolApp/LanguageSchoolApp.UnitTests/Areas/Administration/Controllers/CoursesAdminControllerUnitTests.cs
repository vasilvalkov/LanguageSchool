using AutoMapper;
using LanguageSchoolApp.Areas.Administration.Controllers;
using LanguageSchoolApp.Areas.Administration.Models.Courses;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Models.Courses;
using LanguageSchoolApp.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Areas.Administration.Controllers
{
    [TestFixture]
    public class CoursesAdminControllerUnitTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPassedCourseServiceIsNull()
        {
            // Arrange
            var userServiceStub = new Mock<IUserService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CoursesAdminController(null, userServiceStub.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPassedUserServiceIsNull()
        {
            // Arrange
            var courseServiceStub = new Mock<ICourseService>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new CoursesAdminController(courseServiceStub.Object, null));
        }

        [Test]
        public void Courses_ShouldGetAllAvailableCourses()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseEditViewModel>();
                cfg.CreateMap<CourseEditViewModel, Course>();
            });

            var listOfCourses = new List<Course>()
            {
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)}
            };

            var courseServiceMock = new Mock<ICourseService>();
            courseServiceMock.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceStub = new Mock<IUserService>();

            var controller = new CoursesAdminController(courseServiceMock.Object, userServiceStub.Object);

            // Act
            ViewResult result = controller.Courses() as ViewResult;

            // Assert
            courseServiceMock.Verify(cs => cs.GetAll(), Times.Once);
        }       
    }
}
