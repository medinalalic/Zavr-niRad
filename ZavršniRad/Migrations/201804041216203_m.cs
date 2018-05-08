namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pregleds", t => t.PregledId)
                .Index(t => t.PregledId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToothCharts", "PregledId", "dbo.Pregleds");
            DropIndex("dbo.ToothCharts", new[] { "PregledId" });
            DropTable("dbo.ToothCharts");
        }
    }
}
