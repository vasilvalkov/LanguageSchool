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
using System.Collections;

namespace LanguageSchoolApp.UnitTests.Controllers
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
            var userServiceStub = new Mock<IUserService>();

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void AllCourses_ShouldGetAllAvailableCourses()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
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

            CoursesController controller = new CoursesController(courseServiceMock.Object, userServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

            // Assert
            courseServiceMock.Verify(cs => cs.GetAll(), Times.Once);
        }

        [Test]
        public void AllCourses_ShouldPassCourseListViewModelToView()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

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

            var userServiceStub = new Mock<IUserService>();

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

            // Assert
            Assert.AreEqual("CourseListViewModel", result.Model.GetType().Name);
        }

        [Test]
        public void AllCourses_ViewModel_ShouldHaveAllExistingCourses()
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

            var userServiceStub = new Mock<IUserService>();
            
            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

            // Assert
            Assert.AreEqual(listOfCourses.Count, ((CourseListViewModel)result.ViewData.Model).Courses.Count);
        }

        [Test]
        public void AllCourses_ViewModel_ShouldContainCollectionOfCourseViewModels()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

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

            var userServiceStub = new Mock<IUserService>();

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);

            // Act
            ViewResult result = controller.AllCourses() as ViewResult;

            // Assert
            Assert.IsInstanceOf<ICollection<CourseViewModel>>(
                ((CourseListViewModel)result.ViewData.Model).Courses);
        }

        [Test]
        public void ById_ShouldSearchAvailableCourses_Twice()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();
            Guid otherCoursesId = Guid.NewGuid();

            var listOfCourses = new List<Course>()
            {
                new Course()
                {
                    Id = searchedCourseId
                },
                new Course()
                {
                    Id = otherCoursesId
                }
            };

            var courseServiceMock = new Mock<ICourseService>();
            courseServiceMock.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(us => us.GetCourses(It.IsAny<string>())).Returns(listOfCourses.AsQueryable());

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceMock.Object, userServiceStub.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.ById(searchedCourseId) as ViewResult;

            // Assert
            courseServiceMock.Verify(cs => cs.GetAll(), Times.Exactly(2));
        }

        [Test]
        public void ById_ShouldGetCourseByTheGivenIdParameter()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();
            Guid otherCoursesId = Guid.NewGuid();

            var listOfCourses = new List<Course>()
            {
                new Course()
                {
                    Id = searchedCourseId
                },
                new Course()
                {
                    Id = otherCoursesId
                }
            };

            var courseServiceStub = new Mock<ICourseService>();
            courseServiceStub.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(us => us.GetCourses(It.IsAny<string>())).Returns(listOfCourses.AsQueryable());

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.ById(searchedCourseId) as ViewResult;

            // Assert
            Assert.AreEqual(searchedCourseId, ((CourseByIdViewModel)result.ViewData.Model).CourseId);
            Assert.AreNotEqual(otherCoursesId, ((CourseByIdViewModel)result.ViewData.Model).CourseId);
        }

        [Test]
        public void ById_ShouldSearchCurrentUserCourses_Once()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();
            Guid otherCoursesId = Guid.NewGuid();

            var listOfCourses = new List<Course>()
            {
                new Course()
                {
                    Id = searchedCourseId
                },
                new Course()
                {
                    Id = otherCoursesId
                }
            };

            var courseServiceMock = new Mock<ICourseService>();
            courseServiceMock.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(us => us.GetCourses(It.IsAny<string>())).Returns(listOfCourses.AsQueryable());

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceMock.Object, userServiceMock.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.ById(searchedCourseId) as ViewResult;

            // Assert
            userServiceMock.Verify(cs => cs.GetCourses(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ById_ShouldCallViewNamedCourseInfo()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();
            Guid otherCoursesId = Guid.NewGuid();

            var listOfCourses = new List<Course>()
            {
                new Course()
                {
                    Id = searchedCourseId
                },
                new Course()
                {
                    Id = otherCoursesId
                }
            };

            var courseServiceStub = new Mock<ICourseService>();
            courseServiceStub.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(us => us.GetCourses(It.IsAny<string>())).Returns(listOfCourses.AsQueryable());

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.ById(searchedCourseId) as ViewResult;

            // Assert
            Assert.AreEqual("CourseInfo", result.ViewName);
        }

        [Test]
        public void ById_PassCourseByIdViewModelToView()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();

            var listOfCourses = new List<Course>()
            {
                new Course()
                {
                    Id = searchedCourseId
                }
            };

            var courseServiceStub = new Mock<ICourseService>();
            courseServiceStub.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(us => us.GetCourses(It.IsAny<string>())).Returns(listOfCourses.AsQueryable());

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceStub.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.ById(searchedCourseId) as ViewResult;

            // Assert
            Assert.AreEqual("CourseByIdViewModel", result.Model.GetType().Name);
        }

        [Test]
        public void EnrollStudentInCourse_ShouldSearchAvailableCourses_Once()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();
            Guid otherCoursesId = Guid.NewGuid();

            var listOfCourses = new List<Course>()
            {
                new Course()
                {
                    Id = searchedCourseId
                },
                new Course()
                {
                    Id = otherCoursesId
                }
            };

            var courseServiceMock = new Mock<ICourseService>();
            courseServiceMock.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceStub = new Mock<IUserService>();
            userServiceStub.Setup(us => us.GetCourses(It.IsAny<string>())).Returns(listOfCourses.AsQueryable());

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceMock.Object, userServiceStub.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.EnrollStudentInCourse(searchedCourseId) as ViewResult;

            // Assert
            courseServiceMock.Verify(cs => cs.GetAll(), Times.Exactly(3));
        }

        [Test]
        public void EnrollStudentInCourse_ShouldCallUserServiceEnrollInCourse_Once()
        {
            // Arrange
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();
            });

            Guid searchedCourseId = Guid.NewGuid();
            Course courseToEnroll = new Course() { Id = searchedCourseId };
            var listOfCourses = new List<Course>() { courseToEnroll };

            var courseServiceStub = new Mock<ICourseService>();
            courseServiceStub.Setup(cs => cs.GetAll()).Returns(listOfCourses.AsQueryable());

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(us => us.EnrollInCourse(It.IsAny<string>(), courseToEnroll));

            var controllerContextStub = new Mock<ControllerContext>();
            controllerContextStub.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("Username");
            controllerContextStub.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            CoursesController controller = new CoursesController(courseServiceStub.Object, userServiceMock.Object);
            controller.ControllerContext = controllerContextStub.Object;
            // Act
            ViewResult result = controller.EnrollStudentInCourse(searchedCourseId) as ViewResult;

            // Assert
            userServiceMock.Verify(us => us.EnrollInCourse(It.IsAny<string>(), courseToEnroll), Times.Once);
        }
    }
}
