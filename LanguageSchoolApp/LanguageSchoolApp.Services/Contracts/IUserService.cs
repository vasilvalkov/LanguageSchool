using System.Linq;
using LanguageSchoolApp.Data.Model;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

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
