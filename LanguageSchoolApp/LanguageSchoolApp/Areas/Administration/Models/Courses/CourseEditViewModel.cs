using AutoMapper;
using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApp.Areas.Administration.Models.Courses
{
    public class CourseEditViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartsOn { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndsOn { get; set; }

        public Guid CourseId { get; set; }

        public bool IsDeleted { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseEditViewModel>()
                .ForMember(vm => vm.CourseId, cfg => cfg.MapFrom(course => course.Id));
        }
    }
}
