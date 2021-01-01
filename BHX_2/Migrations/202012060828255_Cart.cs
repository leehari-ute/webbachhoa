namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cart", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.Cart", "Status", c => c.String());
            DropColumn("dbo.Product", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Cart", "Status", c => c.Double(nullable: false));
            DropColumn("dbo.Product", "Amount");
            DropColumn("dbo.Cart", "Amount");
        }
    }
}
