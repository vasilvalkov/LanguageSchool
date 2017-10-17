using Moq;
using System.Web.Mvc;

namespace LanguageSchoolApp.UnitTests
{
    public static class MvcTestExtensions
    {
        public static void InjectContext(this ControllerBase controller, bool ajaxRequest = false)
        {
            var fakeContext = new Mock<ControllerContext>();
            fakeContext.Setup(r => r.HttpContext.Request["X-Requested-With"])
                .Returns(ajaxRequest ? "XMLHttpRequest" : "");
            controller.ControllerContext = fakeContext.Object;
        }
    }
}
