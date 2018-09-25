namespace Customers.Pn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableCustomerIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Customers", new[] { "RelatedEntityId" });
            CreateIndex("dbo.Customers", "RelatedEntityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Customers", new[] { "RelatedEntityId" });
            CreateIndex("dbo.Customers", "RelatedEntityId", unique: true);
        }
    }
}
