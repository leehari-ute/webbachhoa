namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class code2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodeAndInfo", "level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CodeAndInfo", "level");
        }
    }
}
