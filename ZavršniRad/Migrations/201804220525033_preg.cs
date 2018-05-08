namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class preg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pregleds", "IsObavljen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pregleds", "IsObavljen");
        }
    }
}
