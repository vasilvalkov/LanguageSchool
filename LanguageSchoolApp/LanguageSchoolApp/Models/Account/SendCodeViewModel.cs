using System.Collections.Generic;
using System.Web.Mvc;

namespace LanguageSchoolApp.Models.Account
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public virtual ICollection<SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
