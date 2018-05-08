namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nezz : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Porukas", "StomatologPoruka");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Porukas", "StomatologPoruka", c => c.Boolean(nullable: false));
        }
    }
}
