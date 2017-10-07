using LanguageSchoolApp.Controllers;
using LanguageSchoolApp.Services.Contracts;
using System.Web.Mvc;
using System.Web.Routing;

namespace LanguageSchoolApp.UnitTests.Fakes
{
    public class CoursesControllerFake : CoursesController
    {
        public CoursesControllerFake(ICourseService courseService, IUserService userService) 
            : base(courseService, userService)
        {
        }

        protected override RedirectToRouteResult RedirectToAction(string action, string controller, RouteValueDictionary routeValues)
        {
            return new RedirectToRouteResult(string.Format("/{0}/{1}", controller, action), routeValues);
        }
    }
}
