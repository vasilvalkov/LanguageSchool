using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Manage
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Logins = new List<UserLoginInfo>();
            this.Courses = new HashSet<UserCoursesViewModel>();
        }

        public bool HasPassword { get; set; }

        public virtual ICollection<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public virtual ICollection<UserCoursesViewModel> Courses { get; set; }
    }
}
