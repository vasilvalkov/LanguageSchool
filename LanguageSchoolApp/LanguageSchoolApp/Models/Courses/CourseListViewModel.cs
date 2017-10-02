using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Courses
{
    public class CourseListViewModel
    {
        public virtual ICollection<CourseViewModel> Courses { get; set; }
    }
}
