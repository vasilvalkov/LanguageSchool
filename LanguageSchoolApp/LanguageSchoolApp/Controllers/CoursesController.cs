using AutoMapper.QueryableExtensions;
using LanguageSchoolApp.Models.Courses;
using LanguageSchoolApp.Services.Contracts;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LanguageSchoolApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public CoursesController(ICourseService courseService, IUserService userService)
        {
            this.courseService = courseService;
            this.userService = userService;
        }

        [HttpGet]
        public ActionResult AllCourses()
        {
            var allCourses = this.courseService
                .GetAll()
                .OrderBy(x => x.StartsOn)
                .ProjectTo<CourseViewModel>()
                .ToList();

            var viewModel = new CourseListViewModel()
            {
                Courses = allCourses
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult ById(Guid id)
        {
            var course = this.courseService
                .GetAll()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            bool isUserEnrolledInCourse = this.userService
                .GetCourses(this.User.Identity.Name)
                .Contains(course);

            var viewModel = this.courseService
                .GetAll()
                .Where(c => c.Id == id)
                .Select(x => new CourseByIdViewModel
                {
                    Title = x.Title,
                    Description = x.Description,
                    StartsOn = x.StartsOn,
                    EndsOn = x.EndsOn,
                    EnrolledStudentsCount = x.Students.Count(),
                    IsCurrentUserEnrolled = isUserEnrolledInCourse,
                    CourseId = x.Id
                })
                .FirstOrDefault();

            return View("CourseInfo", viewModel);
        }
        
        public ActionResult EnrollStudentInCourse(Guid id)
        {
            var courseToEnroll = this.courseService
                .GetAll()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            this.userService.EnrollInCourse(this.User.Identity.Name, courseToEnroll);

            return this.ById(id);
        }
    }
}