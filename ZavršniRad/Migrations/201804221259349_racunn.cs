namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class racunn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Osobljes", "RacunId", "dbo.Racuns");
            DropForeignKey("dbo.Pacijents", "RacunId", "dbo.Racuns");
            DropIndex("dbo.Pacijents", new[] { "RacunId" });
            DropIndex("dbo.Osobljes", new[] { "RacunId" });
            AddColumn("dbo.Racuns", "OsobljeId", c => c.Int());
            AddColumn("dbo.Racuns", "PacijentId", c => c.Int());
            CreateIndex("dbo.Racuns", "OsobljeId");
            CreateIndex("dbo.Racuns", "PacijentId");
            AddForeignKey("dbo.Racuns", "OsobljeId", "dbo.Osobljes", "Id");
            AddForeignKey("dbo.Racuns", "PacijentId", "dbo.Pacijents", "Id");
            DropColumn("dbo.Pacijents", "RacunId");
            DropColumn("dbo.Osobljes", "RacunId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Osobljes", "RacunId", c => c.Int());
            AddColumn("dbo.Pacijents", "RacunId", c => c.Int());
            DropForeignKey("dbo.Racuns", "PacijentId", "dbo.Pacijents");
            DropForeignKey("dbo.Racuns", "OsobljeId", "dbo.Osobljes");
            DropIndex("dbo.Racuns", new[] { "PacijentId" });
            DropIndex("dbo.Racuns", new[] { "OsobljeId" });
            DropColumn("dbo.Racuns", "PacijentId");
            DropColumn("dbo.Racuns", "OsobljeId");
            CreateIndex("dbo.Osobljes", "RacunId");
            CreateIndex("dbo.Pacijents", "RacunId");
            AddForeignKey("dbo.Pacijents", "RacunId", "dbo.Racuns", "Id");
            AddForeignKey("dbo.Osobljes", "RacunId", "dbo.Racuns", "Id");
        }
    }
}
