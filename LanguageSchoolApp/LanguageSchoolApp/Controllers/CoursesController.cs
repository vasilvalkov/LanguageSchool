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

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public ActionResult AllCourses()
        {
            var allCourses = this.courseService
                .GetAll()
                .OrderBy(x => x.StartsOn)
                .Select(x => new CourseViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    StartsOn = x.StartsOn,
                    EndsOn = x.EndsOn,
                    EnrolledStudentsCount = x.Students.Count(),
                    CourseId = x.Id
                })
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
            var viewModel = this.courseService
                .GetAll()
                .Where(c => c.Id == id)
                .Select(x => new CourseViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    StartsOn = x.StartsOn,
                    EndsOn = x.EndsOn,
                    EnrolledStudentsCount = x.Students.Count(),
                    CourseId = x.Id
                })
                .FirstOrDefault();

            return View("CourseInfo", viewModel);
        }
    }
}