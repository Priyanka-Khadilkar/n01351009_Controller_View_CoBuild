namespace PetGrooming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class petgrooming1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pets", "PetWeight", c => c.Double(nullable: false));
            AddColumn("dbo.Pets", "PetColor", c => c.String());
            AddColumn("dbo.Pets", "PetNotes", c => c.String());
            DropColumn("dbo.Pets", "Weight");
            DropColumn("dbo.Pets", "Color");
            DropColumn("dbo.Pets", "Notes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pets", "Notes", c => c.String());
            AddColumn("dbo.Pets", "Color", c => c.String());
            AddColumn("dbo.Pets", "Weight", c => c.Double(nullable: false));
            DropColumn("dbo.Pets", "PetNotes");
            DropColumn("dbo.Pets", "PetColor");
            DropColumn("dbo.Pets", "PetWeight");
        }
    }
}
