using System.Collections.Generic;
using System.Web.Mvc;

namespace LanguageSchoolApp.Models.Manage
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public virtual ICollection<SelectListItem> Providers { get; set; }
    }
}
