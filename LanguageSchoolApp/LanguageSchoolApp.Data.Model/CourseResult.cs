using LanguageSchoolApp.Data.Model.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApp.Data.Model
{
    public class CourseResult : DataModel
    {
        [Required]
        public virtual Course Course { get; set; }

        [Range(0, 100)]
        public int CoursePoints { get; set; }

        [Required]
        public virtual User Student { get; set; }
    }
}
