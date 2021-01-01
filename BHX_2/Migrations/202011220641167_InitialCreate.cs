namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        ProductGroup = c.String(),
                        Price = c.Single(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Role");
            DropTable("dbo.Product");
        }
    }
}
