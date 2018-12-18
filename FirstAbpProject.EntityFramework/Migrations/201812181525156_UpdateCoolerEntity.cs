namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCoolerEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Coolers", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Coolers", "UserId", "dbo.AbpUsers");
            DropIndex("dbo.Coolers", new[] { "ClientId" });
            DropIndex("dbo.Coolers", new[] { "UserId" });
            AddColumn("dbo.Coolers", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Coolers", "StoreId");
            AddForeignKey("dbo.Coolers", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
            DropColumn("dbo.Coolers", "ClientId");
            DropColumn("dbo.Coolers", "UserId");
            DropColumn("dbo.Coolers", "Province");
            DropColumn("dbo.Coolers", "City");
            DropColumn("dbo.Coolers", "Area");
            DropColumn("dbo.Coolers", "Office");
            DropColumn("dbo.Coolers", "Address");
            DropColumn("dbo.Coolers", "AddressEn");
            DropColumn("dbo.Coolers", "StoreCode");
            DropColumn("dbo.Coolers", "StoreName");
            DropColumn("dbo.Coolers", "StoreNameEn");
            DropColumn("dbo.Coolers", "Gpsx");
            DropColumn("dbo.Coolers", "Gpsy");
            DropColumn("dbo.Coolers", "CustomerId");
            DropColumn("dbo.Coolers", "ProjectId");
            DropColumn("dbo.Coolers", "LastModificationTime");
            DropColumn("dbo.Coolers", "LastModifierUserId");
            DropColumn("dbo.Coolers", "DeletionTime");
            DropColumn("dbo.Coolers", "DeleteUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coolers", "DeleteUserId", c => c.Long());
            AddColumn("dbo.Coolers", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Coolers", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.Coolers", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.Coolers", "ProjectId", c => c.Guid());
            AddColumn("dbo.Coolers", "CustomerId", c => c.Int());
            AddColumn("dbo.Coolers", "Gpsy", c => c.Int(nullable: false));
            AddColumn("dbo.Coolers", "Gpsx", c => c.Int(nullable: false));
            AddColumn("dbo.Coolers", "StoreNameEn", c => c.String());
            AddColumn("dbo.Coolers", "StoreName", c => c.String());
            AddColumn("dbo.Coolers", "StoreCode", c => c.String());
            AddColumn("dbo.Coolers", "AddressEn", c => c.String());
            AddColumn("dbo.Coolers", "Address", c => c.String());
            AddColumn("dbo.Coolers", "Office", c => c.String());
            AddColumn("dbo.Coolers", "Area", c => c.String());
            AddColumn("dbo.Coolers", "City", c => c.String());
            AddColumn("dbo.Coolers", "Province", c => c.String());
            AddColumn("dbo.Coolers", "UserId", c => c.Long(nullable: false));
            AddColumn("dbo.Coolers", "ClientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Coolers", "StoreId", "dbo.Stores");
            DropIndex("dbo.Coolers", new[] { "StoreId" });
            DropColumn("dbo.Coolers", "StoreId");
            CreateIndex("dbo.Coolers", "UserId");
            CreateIndex("dbo.Coolers", "ClientId");
            AddForeignKey("dbo.Coolers", "UserId", "dbo.AbpUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Coolers", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
