namespace TaskBlog.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentPostId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false),
                        DateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.ParentPostId)
                .Index(t => t.ParentPostId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagPost",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.TagId })
                .ForeignKey("dbo.Post", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagPost", "TagId", "dbo.Tag");
            DropForeignKey("dbo.TagPost", "PostId", "dbo.Post");
            DropForeignKey("dbo.Post", "ParentPostId", "dbo.Post");
            DropIndex("dbo.TagPost", new[] { "TagId" });
            DropIndex("dbo.TagPost", new[] { "PostId" });
            DropIndex("dbo.Post", new[] { "ParentPostId" });
            DropTable("dbo.TagPost");
            DropTable("dbo.Tag");
            DropTable("dbo.Post");
        }
    }
}
