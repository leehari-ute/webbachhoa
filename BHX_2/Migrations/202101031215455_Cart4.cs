namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListCart", "ngayBan", c => c.DateTime(nullable: false));
            DropColumn("dbo.Cart", "ngayBan");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cart", "ngayBan", c => c.DateTime(nullable: false));
            DropColumn("dbo.ListCart", "ngayBan");
        }
    }
}
