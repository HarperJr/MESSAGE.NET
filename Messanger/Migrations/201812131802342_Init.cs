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
                        MessageId = c.String(maxLength: 128),
                        MultimediaId = c.String(maxLength: 64),
                        IsStatic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.MessageId)
                .ForeignKey("dbo.Multimedias", t => t.MultimediaId)
                .Index(t => t.MessageId)
                .Index(t => t.MultimediaId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DialogId = c.String(maxLength: 128),
                        SenderId = c.String(maxLength: 128),
                        Time = c.Long(nullable: false),
                        Content = c.String(),
                        HasMultimedia = c.Boolean(nullable: false),
                        Viewed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId)
                .ForeignKey("dbo.Consumers", t => t.SenderId)
                .Index(t => t.DialogId)
                .Index(t => t.SenderId);
            
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        OwnerId = c.String(maxLength: 128),
                        ShortcutId = c.String(maxLength: 64),
                        Title = c.String(maxLength: 32),
                        InitTime = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consumers", t => t.OwnerId)
                .ForeignKey("dbo.Multimedias", t => t.ShortcutId)
                .Index(t => t.OwnerId)
                .Index(t => t.ShortcutId);
            
            CreateTable(
                "dbo.Consumers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 64),
                        AvatarId = c.String(maxLength: 64),
                        PhoneNumber = c.String(maxLength: 16),
                        LastTimeOnline = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Multimedias", t => t.AvatarId)
                .Index(t => t.AvatarId);
            
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
                        InitTime = c.Long(nullable: false),
                        Status = c.String(maxLength: 32),
                        InitialConsumerId = c.String(maxLength: 128),
                        RelatedConsumerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consumers", t => t.InitialConsumerId)
                .ForeignKey("dbo.Consumers", t => t.RelatedConsumerId)
                .Index(t => t.InitialConsumerId)
                .Index(t => t.RelatedConsumerId);
            
            CreateTable(
                "dbo.DialogParticipants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DialogId = c.String(maxLength: 128),
                        ParticipantId = c.String(maxLength: 128),
                        InvitorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dialogs", t => t.DialogId)
                .ForeignKey("dbo.Consumers", t => t.InvitorId)
                .ForeignKey("dbo.Consumers", t => t.ParticipantId)
                .Index(t => t.DialogId)
                .Index(t => t.ParticipantId)
                .Index(t => t.InvitorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DialogParticipants", "ParticipantId", "dbo.Consumers");
            DropForeignKey("dbo.DialogParticipants", "InvitorId", "dbo.Consumers");
            DropForeignKey("dbo.DialogParticipants", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.Contacts", "RelatedConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.Contacts", "InitialConsumerId", "dbo.Consumers");
            DropForeignKey("dbo.AttachedMultimedias", "MultimediaId", "dbo.Multimedias");
            DropForeignKey("dbo.AttachedMultimedias", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Consumers");
            DropForeignKey("dbo.Messages", "DialogId", "dbo.Dialogs");
            DropForeignKey("dbo.Dialogs", "ShortcutId", "dbo.Multimedias");
            DropForeignKey("dbo.Dialogs", "OwnerId", "dbo.Consumers");
            DropForeignKey("dbo.Consumers", "AvatarId", "dbo.Multimedias");
            DropIndex("dbo.DialogParticipants", new[] { "InvitorId" });
            DropIndex("dbo.DialogParticipants", new[] { "ParticipantId" });
            DropIndex("dbo.DialogParticipants", new[] { "DialogId" });
            DropIndex("dbo.Contacts", new[] { "RelatedConsumerId" });
            DropIndex("dbo.Contacts", new[] { "InitialConsumerId" });
            DropIndex("dbo.Consumers", new[] { "AvatarId" });
            DropIndex("dbo.Dialogs", new[] { "ShortcutId" });
            DropIndex("dbo.Dialogs", new[] { "OwnerId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "DialogId" });
            DropIndex("dbo.AttachedMultimedias", new[] { "MultimediaId" });
            DropIndex("dbo.AttachedMultimedias", new[] { "MessageId" });
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
