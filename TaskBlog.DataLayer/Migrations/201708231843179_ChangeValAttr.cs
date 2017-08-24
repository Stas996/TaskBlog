namespace TaskBlog.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeValAttr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.Post", "Description", c => c.String());
            AlterColumn("dbo.Tag", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tag", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Post", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Post", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
