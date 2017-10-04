using System.Linq;
using LanguageSchoolApp.Data.Model;

namespace LanguageSchoolApp.Services.Contracts
{
    public interface IUserService
    {
        IQueryable<User> ByUsername(string username);

        IQueryable<Course> GetCourses(string username);

        string UserIdByUsername(string username);

        void EnrollInCourse(string username, Course course);
    }
}
