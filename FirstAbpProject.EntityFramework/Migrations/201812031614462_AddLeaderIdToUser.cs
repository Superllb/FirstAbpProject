namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLeaderIdToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "LeaderId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "LeaderId");
        }
    }
}
