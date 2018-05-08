namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToothCharts", "PregledId", "dbo.Pregleds");
            DropIndex("dbo.ToothCharts", new[] { "PregledId" });
            AddColumn("dbo.Pacijents", "AddedOn", c => c.DateTime(nullable: false));
            DropTable("dbo.ToothCharts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ToothCharts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Slika = c.Binary(),
                        ImagePath = c.String(),
                        PregledId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Pacijents", "AddedOn");
            CreateIndex("dbo.ToothCharts", "PregledId");
            AddForeignKey("dbo.ToothCharts", "PregledId", "dbo.Pregleds", "Id");
        }
    }
}
