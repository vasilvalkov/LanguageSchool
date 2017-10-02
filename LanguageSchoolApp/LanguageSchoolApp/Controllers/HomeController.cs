using LanguageSchoolApp.Models.Courses;
using LanguageSchoolApp.Models.Home;
using LanguageSchoolApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageSchoolApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseService courseService;

        public HomeController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public ActionResult Index()
        {
            var upcomingCourses = this.courseService
                .GetAll()
                .Where(x => x.StartsOn > DateTime.Now)
                .OrderBy(x => x.StartsOn)
                .Select(x => new CourseTileViewModel()
                {
                    Title = x.Title,
                    Description = x.Description,
                    StartsOn = x.StartsOn,
                    CourseId = x.Id
                })
                .Take(3)
                .ToList();

            var viewModel = new HomeViewModel()
            {
                Courses = upcomingCourses
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}