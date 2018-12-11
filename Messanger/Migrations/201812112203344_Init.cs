namespace Messanger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttachedMultimedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsStatic = c.Boolean(nullable: false),
                        Message_Id = c.String(maxLength: 128),
                        Multimedia_Id = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.Message_Id)
                .ForeignKey("dbo.Multimedias", t => t.Multimedia_Id)
                .Index(t => t.Message_Id)
                .Index(t => t.Multimedia_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                        Content = c.String(),
                        HasMultimedia = c.Boolean(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                        Dialog_Id = c.Int(),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.Dialog_Id)
                .ForeignKey("dbo.Consumers", t => t.Sender_Id)
                .Index(t => t.Dialog_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 32),
                        InitDate = c.DateTime(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        Shortcut_Id = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consumers", t => t.Owner_Id)
                .ForeignKey("dbo.Multimedias", t => t.Shortcut_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Shortcut_Id);
            
            CreateTable(
                "dbo.Consumers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 64),
                        PhoneNumber = c.String(maxLength: 16),
                        LastTimeOnline = c.DateTime(nullable: false),
                        Avatar_Id = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Multimedias", t => t.Avatar_Id)
                .Index(t => t.Avatar_Id);
            
            CreateTable(
                "dbo.Multimedias",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 64),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        RemotePath = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitTime = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 32),
                        InitialConsumer_Id = c.String(maxLength: 128),
                        RelatedConsumer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consumers", t => t.InitialConsumer_Id)
                .ForeignKey("dbo.Consumers", t => t.RelatedConsumer_Id)
                .Index(t => t.InitialConsumer_Id)
                .Index(t => t.RelatedConsumer_Id);
            
            CreateTable(
                "dbo.DialogParticipants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dialog_Id = c.Int(),
                        Invitor_Id = c.String(maxLength: 128),
                        Participant_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.Dialog_Id)
                .ForeignKey("dbo.Consumers", t => t.Invitor_Id)
                .ForeignKey("dbo.Consumers", t => t.Participant_Id)
                .Index(t => t.Dialog_Id)
                .Index(t => t.Invitor_Id)
                .Index(t => t.Participant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DialogParticipants", "Participant_Id", "dbo.Consumers");
            DropForeignKey("dbo.DialogParticipants", "Invitor_Id", "dbo.Consumers");
            DropForeignKey("dbo.DialogParticipants", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Contacts", "RelatedConsumer_Id", "dbo.Consumers");
            DropForeignKey("dbo.Contacts", "InitialConsumer_Id", "dbo.Consumers");
            DropForeignKey("dbo.AttachedMultimedias", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.AttachedMultimedias", "Message_Id", "dbo.Messages");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Consumers");
            DropForeignKey("dbo.Messages", "Dialog_Id", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "Shortcut_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Dialogs", "Owner_Id", "dbo.Consumers");
            DropForeignKey("dbo.Consumers", "Avatar_Id", "dbo.Multimedias");
            DropIndex("dbo.DialogParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.DialogParticipants", new[] { "Invitor_Id" });
            DropIndex("dbo.DialogParticipants", new[] { "Dialog_Id" });
            DropIndex("dbo.Contacts", new[] { "RelatedConsumer_Id" });
            DropIndex("dbo.Contacts", new[] { "InitialConsumer_Id" });
            DropIndex("dbo.Consumers", new[] { "Avatar_Id" });
            DropIndex("dbo.Dialogs", new[] { "Shortcut_Id" });
            DropIndex("dbo.Dialogs", new[] { "Owner_Id" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Dialog_Id" });
            DropIndex("dbo.AttachedMultimedias", new[] { "Multimedia_Id" });
            DropIndex("dbo.AttachedMultimedias", new[] { "Message_Id" });
            DropTable("dbo.DialogParticipants");
            DropTable("dbo.Contacts");
            DropTable("dbo.Multimedias");
            DropTable("dbo.Consumers");
            DropTable("dbo.Dialogs");
            DropTable("dbo.Messages");
            DropTable("dbo.AttachedMultimedias");
        }
    }
}
