using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApp.Models.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
