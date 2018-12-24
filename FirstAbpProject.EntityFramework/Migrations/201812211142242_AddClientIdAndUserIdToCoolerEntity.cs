namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientIdAndUserIdToCoolerEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coolers", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Coolers", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.Coolers", "ClientId");
            CreateIndex("dbo.Coolers", "UserId");
            AddForeignKey("dbo.Coolers", "ClientId", "dbo.Clients", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Coolers", "UserId", "dbo.AbpUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coolers", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Coolers", "ClientId", "dbo.Clients");
            DropIndex("dbo.Coolers", new[] { "UserId" });
            DropIndex("dbo.Coolers", new[] { "ClientId" });
            DropColumn("dbo.Coolers", "UserId");
            DropColumn("dbo.Coolers", "ClientId");
        }
    }
}
