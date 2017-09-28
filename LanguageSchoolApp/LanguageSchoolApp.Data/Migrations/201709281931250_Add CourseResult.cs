namespace LanguageSchoolApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseResults",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CoursePoints = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Course_Id = c.Guid(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Course_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Course_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Course_Id);
            
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            CreateIndex("dbo.AspNetUsers", "IsDeleted");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.UserCourses", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseResults", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseResults", "Course_Id", "dbo.Courses");
            DropIndex("dbo.UserCourses", new[] { "Course_Id" });
            DropIndex("dbo.UserCourses", new[] { "User_Id" });
            DropIndex("dbo.CourseResults", new[] { "User_Id" });
            DropIndex("dbo.CourseResults", new[] { "Course_Id" });
            DropIndex("dbo.CourseResults", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetUsers", new[] { "IsDeleted" });
            DropColumn("dbo.AspNetUsers", "DeletedOn");
            DropColumn("dbo.AspNetUsers", "CreatedOn");
            DropColumn("dbo.AspNetUsers", "ModifiedOn");
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropTable("dbo.UserCourses");
            DropTable("dbo.CourseResults");
        }
    }
}
