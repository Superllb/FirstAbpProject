namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssignClientToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "ClientId", c => c.Int());
            CreateIndex("dbo.AbpUsers", "ClientId");
            AddForeignKey("dbo.AbpUsers", "ClientId", "dbo.Clients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AbpUsers", "ClientId", "dbo.Clients");
            DropIndex("dbo.AbpUsers", new[] { "ClientId" });
            DropColumn("dbo.AbpUsers", "ClientId");
        }
    }
}
