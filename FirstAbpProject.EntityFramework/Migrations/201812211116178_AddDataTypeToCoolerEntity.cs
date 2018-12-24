namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataTypeToCoolerEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coolers", "DataType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coolers", "DataType");
        }
    }
}
