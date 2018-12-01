namespace DataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataFormat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttachedMultimedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsStatic = c.Boolean(nullable: false),
                        Message_Id = c.Int(),
                        Multimedia_Id = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.Message_Id)
                .ForeignKey("dbo.Multimedias", t => t.Multimedia_Id)
                .Index(t => t.Message_Id)
                .Index(t => t.Multimedia_Id);
            
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
            DropIndex("dbo.DialogParticipants", new[] { "Participant_Id" });
            DropIndex("dbo.DialogParticipants", new[] { "Invitor_Id" });
            DropIndex("dbo.DialogParticipants", new[] { "Dialog_Id" });
            DropIndex("dbo.Contacts", new[] { "RelatedConsumer_Id" });
            DropIndex("dbo.Contacts", new[] { "InitialConsumer_Id" });
            DropIndex("dbo.AttachedMultimedias", new[] { "Multimedia_Id" });
            DropIndex("dbo.AttachedMultimedias", new[] { "Message_Id" });
            DropTable("dbo.DialogParticipants");
            DropTable("dbo.Contacts");
            DropTable("dbo.AttachedMultimedias");
        }
    }
}
