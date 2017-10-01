using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Courses
{
    public class CourseListViewModel
    {
        public ICollection<CourseViewModel> Courses { get; set; }
    }
}
