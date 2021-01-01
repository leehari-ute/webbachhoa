namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cart", "IDuser", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cart", "IDuser");
        }
    }
}
