namespace Customers.Pn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatedEntitiesIds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerFieldPns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldId = c.Int(nullable: false),
                        FieldStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FieldPns", t => t.FieldId, cascadeDelete: true)
                .Index(t => t.FieldId);
            
            CreateTable(
                "dbo.FieldPns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerPnSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RelatedEntityGroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerPns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 250),
                        CustomerNo = c.Int(),
                        CompanyName = c.String(maxLength: 250),
                        CompanyAddress = c.String(maxLength: 250),
                        ZipCode = c.String(maxLength: 50),
                        CityName = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 250),
                        Email = c.String(maxLength: 250),
                        ContactPerson = c.String(maxLength: 250),
                        Description = c.String(),
                        RelatedEntityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.RelatedEntityId, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerFieldPns", "FieldId", "dbo.FieldPns");
            DropIndex("dbo.CustomerPns", new[] { "RelatedEntityId" });
            DropIndex("dbo.CustomerFieldPns", new[] { "FieldId" });
            DropTable("dbo.CustomerPns");
            DropTable("dbo.CustomerPnSettings");
            DropTable("dbo.FieldPns");
            DropTable("dbo.CustomerFieldPns");
        }
    }
}
