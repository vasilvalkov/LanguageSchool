using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace LanguageSchoolApp.Models.Courses
{
    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartsOn { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime EndsOn { get; set; }

        public Guid CourseId { get; set; }

        public int EnrolledStudentsCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(vm => vm.EnrolledStudentsCount, cfg => cfg.MapFrom(course => course.Students.Count))
                .ForMember(vm => vm.CourseId, cfg => cfg.MapFrom(course => course.Id));
        }
    }
}