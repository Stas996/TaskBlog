namespace TaskBlog.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldNames : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Post", name: "UserProfileId", newName: "UserId");
            RenameIndex(table: "dbo.Post", name: "IX_UserProfileId", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Post", name: "IX_UserId", newName: "IX_UserProfileId");
            RenameColumn(table: "dbo.Post", name: "UserId", newName: "UserProfileId");
        }
    }
}
