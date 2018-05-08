namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hh : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Usluges", newName: "Osobljes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Osobljes", newName: "Usluges");
        }
    }
}
