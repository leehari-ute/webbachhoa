namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cart", "IDCart", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cart", "IDCart");
        }
    }
}
