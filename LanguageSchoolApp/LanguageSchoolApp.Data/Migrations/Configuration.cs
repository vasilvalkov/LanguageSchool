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
        private const string AdministratorUserName = "admin@linguana.com";
        private const string AdministratorPassword = "aaSS11!!";
        private const string DefaultStudentUserName = "student@linguana.com";
        private const string DefaultStudentPassword = "aaSS11!!";
        private string studentRoleId = string.Empty;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            // Uncomment to debug Seed
            // if (System.Diagnostics.Debugger.IsAttached == false)
            // { 
            //     System.Diagnostics.Debugger.Launch();
            // }

            this.SeedUsers(context);
            this.SeedSampleData(context);
            base.Seed(context);
        }

        public void SeedSampleData(MsSqlDbContext context)
        {
            Random rnd = new Random();

            if (!context.Courses.Any())
            {
                string[] courseTitles = new[] { "Business English", "Italian with Espresso", "Cambridge certification exam", "English for beginners", "Bulgarian for speakers of other languages", "Modern Chinese" };

                string[] courseDescriptions = new[] 
                {
                    "Lorem ipsum dolor sit amet, ei explicari voluptaria cum, vel epicurei theophrastus in. Mel an elit eleifend iracundia, id erat antiopam inimicus pri. Partiendo erroribus ad sed, dolor dictas vocent ne usu, per eu facer errem. Cum nostrum perfecto cu, eos ei essent feugiat eleifend. Vis et nostrum percipit.",
                    "Erant eligendi te pro, vel ut unum recusabo. Eum petentium tincidunt cu, deseruisse posidonium mediocritatem id his, graeco meliore mea ad. Cu vidit homero dissentiet pro. Ex persequeris adversarium usu, est id delicata repudiandae, feugiat conclusionemque nam te. Ad has amet sanctus accommodare, vis recteque persequeris ex. Per id mollis legimus apeirian, usu simul vituperata ut, his no tempor utamur delenit.",
                    "Audiam postulant qui ne, periculis torquatos assentior mel ex, ea mei scripta conceptam. Vulputate theophrastus quo cu, sea alii ridens iracundia no. Torquatos persecuti ex usu. Est tritani virtute referrentur an, eu utamur latine fabulas sit. Populo maiorum nominati sea ut",
                    "Ea cum enim option, etiam eleifend molestiae an has. Has ea novum atomorum maluisset, mel impedit deserunt ad. Eum an imperdiet scripserit, doming regione nam ei, eum at nostro partiendo. His velit nostro quaestio ne. Homero placerat cum ad, ea vim iudicabit aliquando intellegam, te eos partem petentium.",
                    "His ei quem ornatus admodum, no eos eius noster voluptatum. Nam ei ubique accommodare, ullum eripuit eos et, hinc ancillae explicari at quo. Mel in sint natum aliquam, solum solet argumentum te eum. Nam ei nostro oblique feugiat, ne eum nemore consulatu definitionem. Ei nemore albucius imperdiet eam, cu usu natum exerci. Vim doming disputationi an, usu oblique appareat no.",
                    "Eius latine singulis cu est, discere tacimates an sed. Tempor altera ex est. Te nisl nusquam eum, magna euismod usu an, labores civibus sententiae his ex. Eu pri debet offendit, quas porro definitiones ea vis, vel ut volutpat salutandi. Id nihil scribentur mel, et mel altera mucius omittantur, consul vituperatoribus ex eum. Sea at amet fabellas, sea ne labore sadipscing. Ea nominavi interesset vis, affert iisque veritus qui ex. Eros oratio tincidunt ne vix, ut duo quando corrumpit forensibus. Vix odio hinc delicata ei. Iisque delenit phaedrum ex qui, ex nam commodo senserit."
                };

                for (int i = 1; i <= courseTitles.Length; i++)
                {
                    var date = DateTime.Now.AddMonths(rnd.Next(0, 8));
                    date.AddDays(rnd.Next(1, 30));

                    var course = new Course()
                    {
                        Title = courseTitles[i - 1],
                        Description = courseDescriptions[i - 1],
                        CreatedOn = DateTime.Now,
                        StartsOn = date,
                        EndsOn = date.AddDays(30)
                    };

                    if (i % 2 == 1)
                    {
                        course.Students.Add(context.Users.First(u => u.Email == DefaultStudentUserName));
                    }

                    context.Courses.Add(course);

                    if (!context.CourseResults.Any() && i % 2 == 1)
                    {
                        var courseResult = new CourseResult()
                        {
                            Course = course,
                            CoursePoints = rnd.Next(0, 100),
                            Student = context
                                .Users
                                .First(u => u.Email == DefaultStudentUserName &&
                                            u.Roles.FirstOrDefault(r => r.UserId == u.Id &&
                                                                        r.RoleId == this.studentRoleId) != null)
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

                var roleAdmin = new IdentityRole { Name = "Admin" };
                roleManager.Create(roleAdmin);
                var roleStudent = new IdentityRole { Name = "Student" };
                this.studentRoleId = roleStudent.Id;
                roleManager.Create(roleStudent);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);

                var userAdmin = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };
                userManager.Create(userAdmin, AdministratorPassword);
                userManager.AddToRole(userAdmin.Id, "Admin");

                var userStudent = new User
                {
                    UserName = DefaultStudentUserName,
                    Email = DefaultStudentUserName,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.Now
                };
                userManager.Create(userStudent, DefaultStudentPassword);
                userManager.AddToRole(userStudent.Id, "Student");
            }
        }
    }
}
