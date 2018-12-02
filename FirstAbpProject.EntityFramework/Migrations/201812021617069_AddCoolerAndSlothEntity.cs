namespace FirstAbpProject.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoolerAndSlothEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Coolers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        SlothId = c.Int(nullable: false),
                        Province = c.String(),
                        City = c.String(),
                        Area = c.String(),
                        Office = c.String(),
                        Address = c.String(),
                        AddressEn = c.String(),
                        CoolerType = c.String(),
                        CoolerCode = c.String(),
                        StoreCode = c.String(),
                        StoreName = c.String(),
                        StoreNameEn = c.String(),
                        Gpsx = c.Int(nullable: false),
                        Gpsy = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsQa = c.Boolean(nullable: false, defaultValue: true),
                        IsQaResult = c.Boolean(nullable: false, defaultValue: true),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        DeleteUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Cooler_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Sloths", t => t.SlothId, cascadeDelete: true)
                .ForeignKey("dbo.AbpUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.UserId)
                .Index(t => t.SlothId);
            
            CreateTable(
                "dbo.Sloths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModelVersion = c.String(),
                        ModelType = c.Int(nullable: false),
                        JsonVersion = c.String(),
                        Ip = c.String(),
                        CameraCount = c.Int(nullable: false),
                        CameraRowsList = c.String(),
                        Status = c.Int(nullable: false),
                        Gpsx = c.Int(),
                        Gpsy = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastModificationTime = c.DateTime(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Sloth_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coolers", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.Coolers", "SlothId", "dbo.Sloths");
            DropForeignKey("dbo.Coolers", "ClientId", "dbo.Clients");
            DropIndex("dbo.Coolers", new[] { "SlothId" });
            DropIndex("dbo.Coolers", new[] { "UserId" });
            DropIndex("dbo.Coolers", new[] { "ClientId" });
            DropTable("dbo.Sloths",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Sloth_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Coolers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Cooler_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
