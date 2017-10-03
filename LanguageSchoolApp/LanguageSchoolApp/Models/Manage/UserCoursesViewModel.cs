using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApp.Models.Manage
{
    public class UserCoursesViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartsOn { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndsOn { get; set; }

        public Guid CourseId { get; set; }
    }
}
