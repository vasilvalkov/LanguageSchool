using AutoMapper.QueryableExtensions;
using LanguageSchoolApp.Areas.Administration.Models.Courses;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Services.Contracts;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LanguageSchoolApp.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CoursesAdminController : Controller
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;

        public CoursesAdminController(ICourseService courseService, IUserService userService)
        {
            if (courseService == null)
            {
                throw new ArgumentNullException("courseService");
            }

            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            this.courseService = courseService;
            this.userService = userService;
        }

        // GET: Administration/Courses
        public ActionResult Courses()
        {
            var courses = this.GetCoursesFromDb()
                .ToList();

            var viewModel = new CoursesAdminViewModel()
            {
                Courses = courses
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public void UpdateCourse(CourseEditViewModel course)
        {
            Course editedCourse = new Course()
            {
                Title = course.Title,
                Description = course.Description,
                StartsOn = course.StartsOn,
                EndsOn = course.EndsOn,
                IsDeleted = course.IsDeleted,
                Id = course.CourseId
            };

            if (this.Request.IsAjaxRequest())
            {
                this.courseService.Update(editedCourse);
            }

            this.GetDisplayRow(course.CourseId);
        }

        [HttpGet]
        public ActionResult GetEditorRow(Guid id)
        {
            var viewModel = this.GetCoursesFromDb()
                .FirstOrDefault(c => c.CourseId == id);

            return this.PartialView("_CourseRowEditPartial", viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult GetDisplayRow(Guid id)
        {
            var viewModel = this.GetCoursesFromDb()
                .FirstOrDefault(c => c.CourseId == id);

            return this.PartialView("_CourseRowReadPartial", viewModel);
        }

        private IQueryable<CourseEditViewModel> GetCoursesFromDb()
        {
            var coursesFromDb = this.courseService
                .GetAll()
                .ProjectTo<CourseEditViewModel>();

            return coursesFromDb;
        }
    }
}
