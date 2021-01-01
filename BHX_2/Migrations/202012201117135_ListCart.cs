namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListCart",
                c => new
                    {
                        IDCart = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Double(nullable: false),
                        IDuser = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.IDCart);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ListCart");
        }
    }
}
