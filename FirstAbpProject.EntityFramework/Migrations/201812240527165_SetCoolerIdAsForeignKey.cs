namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetCoolerIdAsForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coolers", "SlothId", "dbo.Sloths");
            DropIndex("dbo.Coolers", new[] { "SlothId" });
            AddColumn("dbo.Sloths", "CoolerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sloths", "CoolerId");
            AddForeignKey("dbo.Sloths", "CoolerId", "dbo.Coolers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sloths", "CoolerId", "dbo.Coolers");
            DropIndex("dbo.Sloths", new[] { "CoolerId" });
            DropColumn("dbo.Sloths", "CoolerId");
            CreateIndex("dbo.Coolers", "SlothId");
            AddForeignKey("dbo.Coolers", "SlothId", "dbo.Sloths", "Id", cascadeDelete: true);
        }
    }
}
