using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Data.Repositories;
using LanguageSchoolApp.Services.Contracts;
using System.Linq;

namespace LanguageSchoolApp.Services
{
    public class CourseService : ICourseService
    {
        private readonly IEfRepository<Course> courseRepo;

        public CourseService(IEfRepository<Course> courseRepo)
        {
            this.courseRepo = courseRepo;
        }

        public IQueryable<Course> GetAll()
        {
            return this.courseRepo.AllNotDeleted;
        }
    }
}
