namespace LanguageSchoolApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationsforCourseproperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 80));
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.Courses", "Title");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "Title" });
            AlterColumn("dbo.Courses", "Description", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String());
        }
    }
}
