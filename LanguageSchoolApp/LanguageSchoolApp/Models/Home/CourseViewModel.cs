using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace LanguageSchoolApp.Models.Home
{
    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Desctiption { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime StartsOn { get; set; }

        public string CourseId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(vm => vm.Title, cfg => cfg.MapFrom(course => course.Title))
                .ForMember(vm => vm.Desctiption, cfg => cfg.MapFrom(course => course.Description))
                .ForMember(vm => vm.StartsOn, cfg => cfg.MapFrom(course => course.StartsOn))
                .ForMember(vm => vm.CourseId, cfg => cfg.MapFrom(course => course.Id.ToString()));
        }
    }
}