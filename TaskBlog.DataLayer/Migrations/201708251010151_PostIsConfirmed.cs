namespace TaskBlog.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostIsConfirmed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Post", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.Post", new[] { "UserId" });
            AddColumn("dbo.Post", "IsConfirmed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Post", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Post", "UserId");
            AddForeignKey("dbo.Post", "UserId", "dbo.UserProfiles", "Id", cascadeDelete: true);

            //DropForeignKey("dbo.Post", "ParentPostId", "dbo.Post");
            //DropIndex("dbo.Post", new[] { "ParentPostId" });
            //CreateIndex("dbo.Post", "ParentPostId");
            //AddForeignKey("dbo.Post", "ParentPostId", "dbo.Post", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "UserId", "dbo.UserProfiles");
            DropIndex("dbo.Post", new[] { "UserId" });
            AlterColumn("dbo.Post", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Post", "IsConfirmed");
            CreateIndex("dbo.Post", "UserId");
            AddForeignKey("dbo.Post", "UserId", "dbo.UserProfiles", "Id");

            //DropForeignKey("dbo.Post", "ParentPostId", "dbo.Post");
            //DropIndex("dbo.Post", new[] { "ParentPostId" });
            //CreateIndex("dbo.Post", "ParentPostId");
            //AddForeignKey("dbo.Post", "ParentPostId", "dbo.Post", "Id", cascadeDelete: false);
        }
    }
}
