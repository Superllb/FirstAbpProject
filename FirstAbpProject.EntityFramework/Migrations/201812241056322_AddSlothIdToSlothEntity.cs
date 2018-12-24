namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlothIdToSlothEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sloths", "SlothId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sloths", "SlothId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sloths", new[] { "SlothId" });
            DropColumn("dbo.Sloths", "SlothId");
        }
    }
}
