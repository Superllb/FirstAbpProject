namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoreEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        Province = c.String(),
                        City = c.String(),
                        Area = c.String(),
                        Office = c.String(),
                        Address = c.String(),
                        AddressEn = c.String(),
                        StoreCode = c.String(),
                        StoreName = c.String(),
                        StoreNameEn = c.String(),
                        Gpsx = c.Int(nullable: false),
                        Gpsy = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stores", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Stores", "ClientId", "dbo.Clients");
            DropIndex("dbo.Stores", new[] { "UserId" });
            DropIndex("dbo.Stores", new[] { "ClientId" });
            DropTable("dbo.Stores");
        }
    }
}
