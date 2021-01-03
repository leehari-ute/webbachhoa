namespace BHX_2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeAndInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeAndInfo",
                c => new
                    {
                        code = c.Int(nullable: false),
                        ID = c.Int(nullable: false, identity: true),
                        newPass = c.String(),
                        newMail = c.String(),
                        newPhone = c.String(),
                        newBirth = c.DateTime(nullable: false),
                        newAdd = c.String(),
                        newName = c.String(),
                    })
                .PrimaryKey(t => t.code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CodeAndInfo");
        }
    }
}
