using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public HomeController(ICourseService courseService, IMapper mapper)
        {
            this.courseService = courseService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            int upcomingCoursesCount = 3;

            var upcomingCourses = this.courseService
                .GetAll()
                .Where(x => x.StartsOn > DateTime.Now)
                .OrderBy(x => x.StartsOn)
                .ProjectTo<CourseTileViewModel>()
                .Take(upcomingCoursesCount)
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