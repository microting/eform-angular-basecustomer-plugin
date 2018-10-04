namespace Customers.Pn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerNoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerNo", c => c.Int());
        }
    }
}
