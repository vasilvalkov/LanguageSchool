using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Home
{
    public class HomeViewModel
    {
        public ICollection<CourseViewModel> Courses { get; set; }
    }
}