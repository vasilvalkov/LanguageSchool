using LanguageSchoolApp.Data.Model;
using LanguageSchoolApp.Data.Repositories;
using LanguageSchoolApp.Data.SaveContext;
using LanguageSchoolApp.Services.Contracts;
using System.Linq;

namespace LanguageSchoolApp.Services
{
    public class UserService : IUserService
    {
        private readonly ISaveContext context;
        private readonly IEfRepository<User> userRepo;

        public UserService(IEfRepository<User> userRepo, ISaveContext context)
        {
            this.userRepo = userRepo;
            this.context = context;
        }

        public string UserIdByUsername(string username)
        {
            return this.userRepo
                .AllNotDeleted
                .Where(u => u.UserName == username)
                .Select(u => u.Id)
                .FirstOrDefault();
        }

        //public bool UserIsAdmin(string username)
        //{
        //    return this.userRepo
        //        .AllNotDeleted
        //        .Where(u => u.UserName == username)
        //        .Select(u => u.IsAdmin)
        //        .FirstOrDefaultAsync();
        //}

        public IQueryable<User> ByUsername(string username)
        {
            return this.userRepo
                .AllNotDeleted
                .Where(u => u.UserName == username);
        }

        public IQueryable<Course> GetCourses(string username)
        {
            return this.ByUsername(username)
                .Select(u => u.Courses)
                .FirstOrDefault()
                .AsQueryable();
        }

        public void EnrollInCourse(string username, Course course)
        {
            this.ByUsername(username)
                .FirstOrDefault()
                .Courses
                .Add(course);

            this.context.Commit();
        }
    }
}
