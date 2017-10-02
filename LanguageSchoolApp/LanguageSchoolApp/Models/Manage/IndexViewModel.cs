using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Manage
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public virtual IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}
