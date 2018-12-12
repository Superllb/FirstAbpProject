namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerIdAndProjectIdToCooler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coolers", "CustomerId", c => c.Int());
            AddColumn("dbo.Coolers", "ProjectId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coolers", "ProjectId");
            DropColumn("dbo.Coolers", "CustomerId");
        }
    }
}
