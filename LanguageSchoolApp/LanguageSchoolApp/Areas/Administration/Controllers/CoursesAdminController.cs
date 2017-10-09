using LanguageSchoolApp.Areas.Administration.Models.Courses;
using LanguageSchoolApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            var courses = this.courseService.GetAll().ToList();

            var viewModel = new CoursesAdminViewModel()
            {
                Courses = courses
            };

            return this.View(viewModel);
        }


        public JsonResult GetCourses()
        {
            if (this.Request.IsAjaxRequest())
            {
                var coursesJson = this.courseService.GetAll().ToList();
                return Json(new { rows = coursesJson }, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
    }
}