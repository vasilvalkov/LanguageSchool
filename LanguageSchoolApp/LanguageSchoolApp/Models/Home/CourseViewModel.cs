using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApp.Models.Home
{
    public class CourseViewModel
    {
        public string Title { get; set; }

        public string Desctiption { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartsOn { get; set; }

        public string CourseId { get; set; }
    }
}