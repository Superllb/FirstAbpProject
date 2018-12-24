namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSlothIdForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coolers", "SlothId", "dbo.Sloths");
            DropIndex("dbo.Coolers", new[] { "SlothId" });
            AddColumn("dbo.Sloths", "CoolerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sloths", "CoolerId");
            CreateIndex("dbo.Coolers", "SlothId");
            AddForeignKey("dbo.Coolers", "SlothId", "dbo.Sloths", "Id", cascadeDelete: true);
        }
    }
}
