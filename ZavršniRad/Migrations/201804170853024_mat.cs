namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IzlazIzSkladistas", "IzvrsenaUslugaId", "dbo.IzvrsenaUslugas");
            DropForeignKey("dbo.IzlazIzSkladistas", "MaterijalId", "dbo.Materijals");
            DropForeignKey("dbo.IzlazIzSkladistas", "OsobljeId", "dbo.Osobljes");
            DropForeignKey("dbo.IzlazIzSkladistas", "SkladisteId", "dbo.Skladistes");
            DropForeignKey("dbo.StavkeUlazas", "MaterijalId", "dbo.Materijals");
            DropForeignKey("dbo.UlazUSkladistes", "OsobljeId", "dbo.Osobljes");
            DropForeignKey("dbo.UlazUSkladistes", "SkladisteId", "dbo.Skladistes");
            DropForeignKey("dbo.StavkeUlazas", "UlazUSkladisteId", "dbo.UlazUSkladistes");
            DropIndex("dbo.IzlazIzSkladistas", new[] { "MaterijalId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "SkladisteId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "OsobljeId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "IzvrsenaUslugaId" });
            DropIndex("dbo.StavkeUlazas", new[] { "MaterijalId" });
            DropIndex("dbo.StavkeUlazas", new[] { "UlazUSkladisteId" });
            DropIndex("dbo.UlazUSkladistes", new[] { "OsobljeId" });
            DropIndex("dbo.UlazUSkladistes", new[] { "SkladisteId" });
            CreateTable(
                "dbo.PotroseniMaterijals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        OsobljeId = c.Int(nullable: false),
                        MaterijalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materijals", t => t.MaterijalId)
                .ForeignKey("dbo.Osobljes", t => t.OsobljeId)
                .Index(t => t.OsobljeId)
                .Index(t => t.MaterijalId);
            
            AddColumn("dbo.Materijals", "Cijena", c => c.Double(nullable: false));
            DropTable("dbo.IzlazIzSkladistas");
            DropTable("dbo.Skladistes");
            DropTable("dbo.StavkeUlazas");
            DropTable("dbo.UlazUSkladistes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UlazUSkladistes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojFakture = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        IznosRacuna = c.Single(nullable: false),
                        PDV = c.Single(nullable: false),
                        OsobljeId = c.Int(nullable: false),
                        SkladisteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StavkeUlazas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Int(nullable: false),
                        Cijena = c.Double(nullable: false),
                        MaterijalId = c.Int(nullable: false),
                        UlazUSkladisteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skladistes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NazivRobe = c.String(),
                        Kolicina = c.Int(nullable: false),
                        Vrsta = c.String(),
                        Opis = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IzlazIzSkladistas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojRacuna = c.Int(nullable: false),
                        Datum = c.DateTime(nullable: false),
                        Zakljucen = c.Boolean(nullable: false),
                        Uplacen = c.Single(nullable: false),
                        Isplacen = c.Single(nullable: false),
                        MaterijalId = c.Int(nullable: false),
                        SkladisteId = c.Int(nullable: false),
                        OsobljeId = c.Int(nullable: false),
                        IzvrsenaUslugaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PotroseniMaterijals", "OsobljeId", "dbo.Osobljes");
            DropForeignKey("dbo.PotroseniMaterijals", "MaterijalId", "dbo.Materijals");
            DropIndex("dbo.PotroseniMaterijals", new[] { "MaterijalId" });
            DropIndex("dbo.PotroseniMaterijals", new[] { "OsobljeId" });
            DropColumn("dbo.Materijals", "Cijena");
            DropTable("dbo.PotroseniMaterijals");
            CreateIndex("dbo.UlazUSkladistes", "SkladisteId");
            CreateIndex("dbo.UlazUSkladistes", "OsobljeId");
            CreateIndex("dbo.StavkeUlazas", "UlazUSkladisteId");
            CreateIndex("dbo.StavkeUlazas", "MaterijalId");
            CreateIndex("dbo.IzlazIzSkladistas", "IzvrsenaUslugaId");
            CreateIndex("dbo.IzlazIzSkladistas", "OsobljeId");
            CreateIndex("dbo.IzlazIzSkladistas", "SkladisteId");
            CreateIndex("dbo.IzlazIzSkladistas", "MaterijalId");
            AddForeignKey("dbo.StavkeUlazas", "UlazUSkladisteId", "dbo.UlazUSkladistes", "Id");
            AddForeignKey("dbo.UlazUSkladistes", "SkladisteId", "dbo.Skladistes", "Id");
            AddForeignKey("dbo.UlazUSkladistes", "OsobljeId", "dbo.Osobljes", "Id");
            AddForeignKey("dbo.StavkeUlazas", "MaterijalId", "dbo.Materijals", "Id");
            AddForeignKey("dbo.IzlazIzSkladistas", "SkladisteId", "dbo.Skladistes", "Id");
            AddForeignKey("dbo.IzlazIzSkladistas", "OsobljeId", "dbo.Osobljes", "Id");
            AddForeignKey("dbo.IzlazIzSkladistas", "MaterijalId", "dbo.Materijals", "Id");
            AddForeignKey("dbo.IzlazIzSkladistas", "IzvrsenaUslugaId", "dbo.IzvrsenaUslugas", "Id");
        }
    }
}
