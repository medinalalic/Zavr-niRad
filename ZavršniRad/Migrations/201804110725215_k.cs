namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class k : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korisniks", "LozinkaHash", c => c.String());
            AddColumn("dbo.Korisniks", "LozinkaSalt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Korisniks", "LozinkaSalt");
            DropColumn("dbo.Korisniks", "LozinkaHash");
        }
    }
}
