namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shortcat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dialogs", "Shortcut_Id", c => c.String(maxLength: 64));
            CreateIndex("dbo.Dialogs", "Shortcut_Id");
            AddForeignKey("dbo.Dialogs", "Shortcut_Id", "dbo.Multimedias", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dialogs", "Shortcut_Id", "dbo.Multimedias");
            DropIndex("dbo.Dialogs", new[] { "Shortcut_Id" });
            DropColumn("dbo.Dialogs", "Shortcut_Id");
        }
    }
}
