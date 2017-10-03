using AutoMapper;
using LanguageSchoolApp.Controllers;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Models.Courses;
using LanguageSchoolApp.Services.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests.Controllers.Courses
{
    [TestFixture]
    public class CourseControllerUnitTests
    {
        [Test]
        public void AllCourses_ShouldReturnNonNullResult()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });
            var courseServiceStub = new Mock<ICourseService>();
            var mapperStub = new Mock<IMapper>();

            CoursesController controller = new CoursesController(courseServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

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
                new Course() { StartsOn = new DateTime(5540,2,2)},
                new Course() { StartsOn = new DateTime(5540,2,2)}
            };

            var courseServiceStub = new Mock<ICourseService>();
            courseServiceStub.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var mapperStub = new Mock<IMapper>();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            CoursesController controller = new CoursesController(courseServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

            // Assert
            Assert.AreEqual(listOfCourses.Count, ((CourseListViewModel)result.ViewData.Model).Courses.Count);
        }
    }
}
