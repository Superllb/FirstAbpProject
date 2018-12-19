namespace FirstAbpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyIsDeleteToIsDeletedOfStore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "IsDeleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Stores", "IsDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "IsDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Stores", "IsDeleted");
        }
    }
}
