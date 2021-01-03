namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bhxDBSchema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductGroup = c.String(),
                        ProductName = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Image = c.Binary(),
                        UrlImage = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        Email = c.String(),
                        Phone = c.Int(nullable: false),
                        Lever = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.idUser, t.Username });
            
            CreateTable(
                "dbo.Water",
                c => new
                    {
                        WaterID = c.Int(nullable: false, identity: true),
                        WaterName = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Price = c.Single(nullable: false),
                        Image = c.Binary(),
                        UrlImage = c.String(),
                    })
                .PrimaryKey(t => t.WaterID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Water");
            DropTable("dbo.Users");
            DropTable("dbo.Role");
            DropTable("dbo.Product");
        }
    }
}
