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
            var viewModel = this.courseService
                .GetAll()
                .Where(c => c.Id == id)
                .ProjectTo<CourseViewModel>()
                .FirstOrDefault();

            return View("CourseInfo", viewModel);
        }
    }
}