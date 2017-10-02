using System.Data.Entity;
using LanguageSchoolApp.Data.Model;

namespace LanguageSchoolApp.Data
{
    public interface IMsSqlDbContext
    {
        IDbSet<CourseResult> CourseResults { get; set; }
        IDbSet<Course> Courses { get; set; }

        int SaveChanges();
    }
}