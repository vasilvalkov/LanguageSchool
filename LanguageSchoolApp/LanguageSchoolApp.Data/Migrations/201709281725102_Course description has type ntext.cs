namespace LanguageSchoolApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coursedescriptionhastypentext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String(nullable: false));
        }
    }
}
