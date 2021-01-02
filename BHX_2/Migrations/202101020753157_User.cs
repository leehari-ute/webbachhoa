namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "HoTen", c => c.String());
            AddColumn("dbo.Users", "ngaySinh", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "DiaChi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DiaChi");
            DropColumn("dbo.Users", "ngaySinh");
            DropColumn("dbo.Users", "HoTen");
        }
    }
}
