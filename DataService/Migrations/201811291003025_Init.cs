namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consumers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 64),
                        PhoneNumber = c.String(maxLength: 16),
                        LastTimeOnline = c.DateTime(nullable: false),
                        Avatar_Id = c.String(maxLength: 64),
                        Consumer_Id = c.String(maxLength: 128),
                        Dialog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Multimedias", t => t.Avatar_Id)
                .ForeignKey("dbo.Consumers", t => t.Consumer_Id)
                .ForeignKey("dbo.Dialogs", t => t.Dialog_Id)
                .Index(t => t.Avatar_Id)
                .Index(t => t.Consumer_Id)
                .Index(t => t.Dialog_Id);
            
            CreateTable(
                "dbo.Multimedias",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 64),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        RemotePath = c.String(maxLength: 64),
                        Message_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.Message_Id)
                .Index(t => t.Message_Id);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 32),
                        InitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DialogId = c.Int(nullable: false),
                        SenderId = c.String(maxLength: 64),
                        Time = c.DateTime(nullable: false),
                        Content = c.String(),
                        HasMultimedia = c.Boolean(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Multimedias", "Message_Id", "dbo.Messages");
            DropForeignKey("dbo.Consumers", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Consumers", "Consumer_Id", "dbo.Consumers");
            DropForeignKey("dbo.Consumers", "Avatar_Id", "dbo.Multimedias");
            DropIndex("dbo.Multimedias", new[] { "Message_Id" });
            DropIndex("dbo.Consumers", new[] { "Dialog_Id" });
            DropIndex("dbo.Consumers", new[] { "Consumer_Id" });
            DropIndex("dbo.Consumers", new[] { "Avatar_Id" });
            DropTable("dbo.Messages");
            DropTable("dbo.Dialogs");
            DropTable("dbo.Multimedias");
            DropTable("dbo.Consumers");
        }
    }
}
