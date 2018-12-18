namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGpsxyToLongititudeAndLatitude : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Longitude", c => c.Single(nullable: false));
            AddColumn("dbo.Stores", "Latitude", c => c.Single(nullable: false));
            DropColumn("dbo.Stores", "Gpsx");
            DropColumn("dbo.Stores", "Gpsy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "Gpsy", c => c.Single(nullable: false));
            AddColumn("dbo.Stores", "Gpsx", c => c.Single(nullable: false));
            DropColumn("dbo.Stores", "Latitude");
            DropColumn("dbo.Stores", "Longitude");
        }
    }
}
