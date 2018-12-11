namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsOnlineAndReomveIsQaResultOfCooler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coolers", "IsOnline", c => c.Boolean(nullable: false, defaultValue: true));
            DropColumn("dbo.Coolers", "IsQaResult");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coolers", "IsQaResult", c => c.Boolean(nullable: false));
            DropColumn("dbo.Coolers", "IsOnline");
        }
    }
}
