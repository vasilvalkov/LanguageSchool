using LanguageSchoolApp.Data.Model.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageSchoolApp.Data.Model
{
    public class Course : DataModel
    {
        private ICollection<User> students;

        public Course()
        {
            this.students = new HashSet<User>();
        }

        [Index]
        [Required]
        [StringLength(80)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime EndsOn { get; set; }

        public ICollection<User> Students
        {
            get
            {
                return this.students;
            }

            set
            {
                this.students = value;
            }
        }
    }
}
