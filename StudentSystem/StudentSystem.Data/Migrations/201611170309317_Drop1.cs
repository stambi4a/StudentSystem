namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Drop1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Licenses", new[] { "ResourceId" });
            AlterColumn("dbo.Licenses", "ResourceId", c => c.Int());
            CreateIndex("dbo.Licenses", "ResourceId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Licenses", new[] { "ResourceId" });
            AlterColumn("dbo.Licenses", "ResourceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Licenses", "ResourceId");
        }
    }
}
