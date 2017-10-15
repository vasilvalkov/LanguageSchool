using System.Linq;
using LanguageSchoolApp.Data.Model;

namespace LanguageSchoolApp.Services.Contracts
{
    public interface ICourseService
    {
        IQueryable<Course> GetAll();

        void Update(Course course);
    }
}