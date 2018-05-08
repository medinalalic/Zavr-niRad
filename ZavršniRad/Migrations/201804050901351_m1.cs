namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Osobljes", newName: "Usluges");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Usluges", newName: "Osobljes");
        }
    }
}
