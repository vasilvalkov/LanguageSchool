using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;

namespace LanguageSchoolApp.Models.Manage
{
    public class ManageLoginsViewModel
    {
        public virtual IList<UserLoginInfo> CurrentLogins { get; set; }
        public virtual IList<AuthenticationDescription> OtherLogins { get; set; }
    }
}
