using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Data.Repositories;
using LanguageSchoolApp.Data.SaveContext;
using LanguageSchoolApp.Services.Contracts;
using System.Linq;

namespace LanguageSchoolApp.Services
{
    public class CourseService : ICourseService
    {
        private readonly ISaveContext context;
        private readonly IEfRepository<Course> courseRepo;

        public CourseService(IEfRepository<Course> courseRepo, ISaveContext context)
        {
            this.courseRepo = courseRepo;
            this.context = context;
        }

        public IQueryable<Course> GetAll()
        {
            return this.courseRepo.AllNotDeleted;
        }

        public void Update(Course course)
        {
            this.courseRepo.Update(course);
            this.context.Commit();
        }
    }
}
