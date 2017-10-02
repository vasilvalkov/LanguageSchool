using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Home
{
    public class HomeViewModel
    {
        public virtual ICollection<CourseTileViewModel> Courses { get; set; }
    }
}
