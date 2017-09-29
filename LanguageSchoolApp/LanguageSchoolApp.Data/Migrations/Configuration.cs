using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LanguageSchoolApp.Data.Model;
using System.Data.Entity.Migrations;
using System.Linq;
using System;

namespace LanguageSchoolApp.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        const string AdministratorUserName = "admin@linguana.com";
        const string AdministratorPassword = "aaSS11!!";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);
            this.SeedSampleData(context);
            base.Seed(context);
        }

        public void SeedSampleData(MsSqlDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
            Random rnd = new Random();
            if (!context.Courses.Any())
            {
                for (int i = 1; i < 6; i++)
                {
                    var date = new DateTime(2017, 7 + i, 10 + i * 2);
                    var course = new Course()
                    {
                        Title = "Sample Course " + i,
                        Description = "Lorem ipsum dolor sit amet, ei explicari voluptaria cum, vel epicurei theophrastus in. Mel an elit eleifend iracundia, id erat antiopam inimicus pri. Partiendo erroribus ad sed, dolor dictas vocent ne usu, per eu facer errem. Cum nostrum perfecto cu, eos ei essent feugiat eleifend. Vis et nostrum percipit.",
                        CreatedOn = DateTime.Now,
                        StartsOn = date,
                        EndsOn = date.AddDays(30)
                    };

                    course.Students.Add(context.Users.First(u => u.Email == AdministratorUserName));

                    context.Courses.Add(course);

                    if (!context.CourseResults.Any())
                    {
                        var courseResult = new CourseResult()
                        {
                            Course = course,
                            CoursePoints = rnd.Next(0, 100),
                            Student = context.Users.First(u => u.Email == AdministratorUserName)
                        };

                        context.CourseResults.Add(courseResult);
                    }
                }
            }
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
