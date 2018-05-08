namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nova : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IzvrsenaUslugas", t => t.IzvrsenaUslugaId)
                .ForeignKey("dbo.Materijals", t => t.MaterijalId)
                .ForeignKey("dbo.Osobljes", t => t.OsobljeId)
                .ForeignKey("dbo.Skladistes", t => t.SkladisteId)
                .Index(t => t.MaterijalId)
                .Index(t => t.SkladisteId)
                .Index(t => t.OsobljeId)
                .Index(t => t.IzvrsenaUslugaId);
            
            CreateTable(
                "dbo.Materijals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
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
                "dbo.Ocjenas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumOcjenjivanja = c.DateTime(nullable: false),
                        OcjenaInt = c.Int(nullable: false),
                        UslugaId = c.Int(nullable: false),
                        PacijentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pacijents", t => t.PacijentId)
                .ForeignKey("dbo.Uslugas", t => t.UslugaId)
                .Index(t => t.UslugaId)
                .Index(t => t.PacijentId);
            
            CreateTable(
                "dbo.Porukas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PacijentId = c.Int(nullable: false),
                        StomatologId = c.Int(nullable: false),
                        From = c.Int(nullable: false),
                        To = c.Int(),
                        TekstPoruke = c.String(),
                        Datum = c.DateTime(nullable: false),
                        Procitana = c.Boolean(nullable: false),
                        StomatologPoruka = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pacijents", t => t.PacijentId)
                .ForeignKey("dbo.Stomatologs", t => t.StomatologId)
                .Index(t => t.PacijentId)
                .Index(t => t.StomatologId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materijals", t => t.MaterijalId)
                .ForeignKey("dbo.UlazUSkladistes", t => t.UlazUSkladisteId)
                .Index(t => t.MaterijalId)
                .Index(t => t.UlazUSkladisteId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Osobljes", t => t.OsobljeId)
                .ForeignKey("dbo.Skladistes", t => t.SkladisteId)
                .Index(t => t.OsobljeId)
                .Index(t => t.SkladisteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkeUlazas", "UlazUSkladisteId", "dbo.UlazUSkladistes");
            DropForeignKey("dbo.UlazUSkladistes", "SkladisteId", "dbo.Skladistes");
            DropForeignKey("dbo.UlazUSkladistes", "OsobljeId", "dbo.Osobljes");
            DropForeignKey("dbo.StavkeUlazas", "MaterijalId", "dbo.Materijals");
            DropForeignKey("dbo.Porukas", "StomatologId", "dbo.Stomatologs");
            DropForeignKey("dbo.Porukas", "PacijentId", "dbo.Pacijents");
            DropForeignKey("dbo.Ocjenas", "UslugaId", "dbo.Uslugas");
            DropForeignKey("dbo.Ocjenas", "PacijentId", "dbo.Pacijents");
            DropForeignKey("dbo.IzlazIzSkladistas", "SkladisteId", "dbo.Skladistes");
            DropForeignKey("dbo.IzlazIzSkladistas", "OsobljeId", "dbo.Osobljes");
            DropForeignKey("dbo.IzlazIzSkladistas", "MaterijalId", "dbo.Materijals");
            DropForeignKey("dbo.IzlazIzSkladistas", "IzvrsenaUslugaId", "dbo.IzvrsenaUslugas");
            DropIndex("dbo.UlazUSkladistes", new[] { "SkladisteId" });
            DropIndex("dbo.UlazUSkladistes", new[] { "OsobljeId" });
            DropIndex("dbo.StavkeUlazas", new[] { "UlazUSkladisteId" });
            DropIndex("dbo.StavkeUlazas", new[] { "MaterijalId" });
            DropIndex("dbo.Porukas", new[] { "StomatologId" });
            DropIndex("dbo.Porukas", new[] { "PacijentId" });
            DropIndex("dbo.Ocjenas", new[] { "PacijentId" });
            DropIndex("dbo.Ocjenas", new[] { "UslugaId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "IzvrsenaUslugaId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "OsobljeId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "SkladisteId" });
            DropIndex("dbo.IzlazIzSkladistas", new[] { "MaterijalId" });
            DropTable("dbo.UlazUSkladistes");
            DropTable("dbo.StavkeUlazas");
            DropTable("dbo.Porukas");
            DropTable("dbo.Ocjenas");
            DropTable("dbo.Skladistes");
            DropTable("dbo.Materijals");
            DropTable("dbo.IzlazIzSkladistas");
        }
    }
}
