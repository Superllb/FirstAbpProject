namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGpsxyFromIntToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stores", "Gpsx", c => c.Single(nullable: false));
            AlterColumn("dbo.Stores", "Gpsy", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "Gpsy", c => c.Int(nullable: false));
            AlterColumn("dbo.Stores", "Gpsx", c => c.Int(nullable: false));
        }
    }
}
