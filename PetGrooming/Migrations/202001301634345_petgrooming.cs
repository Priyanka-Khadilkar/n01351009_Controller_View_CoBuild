namespace PetGrooming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class petgrooming : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Species", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Species", "Name", c => c.String());
        }
    }
}
