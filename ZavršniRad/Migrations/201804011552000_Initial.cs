namespace ZavršniRad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dijagnozas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IzvrsenaUslugas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PregledId = c.Int(nullable: false),
                        UslugaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pregleds", t => t.PregledId)
                .ForeignKey("dbo.Uslugas", t => t.UslugaId)
                .Index(t => t.PregledId)
                .Index(t => t.UslugaId);
            
            CreateTable(
                "dbo.Pregleds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumPregleda = c.DateTime(nullable: false),
                        VrijemePregleda = c.DateTime(nullable: false),
                        PacijentId = c.Int(nullable: false),
                        StomatologId = c.Int(nullable: false),
                        TerminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pacijents", t => t.PacijentId)
                .ForeignKey("dbo.Stomatologs", t => t.StomatologId)
                .ForeignKey("dbo.Termins", t => t.TerminId)
                .Index(t => t.PacijentId)
                .Index(t => t.StomatologId)
                .Index(t => t.TerminId);
            
            CreateTable(
                "dbo.Pacijents",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        JMBG = c.String(),
                        RacunId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisniks", t => t.Id)
                .ForeignKey("dbo.Racuns", t => t.RacunId)
                .Index(t => t.Id)
                .Index(t => t.RacunId);
            
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Email = c.String(),
                        Mobitel = c.String(),
                        Adresa = c.String(),
                        KorisnickoIme = c.String(),
                        Lozinka = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Aktivan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Osobljes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Titula = c.String(),
                        JMBG = c.String(),
                        RacunId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Racuns", t => t.RacunId)
                .ForeignKey("dbo.Korisniks", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.RacunId);
            
            CreateTable(
                "dbo.Racuns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uplaćeno = c.Boolean(nullable: false),
                        Cijena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Datum = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stomatologs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Titula = c.String(),
                        JMBG = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisniks", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Termins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Razlog = c.String(),
                        Datum = c.DateTime(nullable: false),
                        Vrijeme = c.DateTime(nullable: false),
                        Odobren = c.Boolean(nullable: false),
                        PacijentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pacijents", t => t.PacijentId)
                .Index(t => t.PacijentId);
            
            CreateTable(
                "dbo.Uslugas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vrsta = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lijeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Terapijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vrsta = c.Int(nullable: false),
                        Količina = c.Int(nullable: false),
                        PregledId = c.Int(nullable: false),
                        LijekId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lijeks", t => t.LijekId)
                .ForeignKey("dbo.Pregleds", t => t.PregledId)
                .Index(t => t.PregledId)
                .Index(t => t.LijekId);
            
            CreateTable(
                "dbo.UspostavljenaDijagnozas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Intenzitet = c.Int(nullable: false),
                        Napomena = c.String(),
                        PregledId = c.Int(nullable: false),
                        DijagnozaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dijagnozas", t => t.DijagnozaId)
                .ForeignKey("dbo.Pregleds", t => t.PregledId)
                .Index(t => t.PregledId)
                .Index(t => t.DijagnozaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UspostavljenaDijagnozas", "PregledId", "dbo.Pregleds");
            DropForeignKey("dbo.UspostavljenaDijagnozas", "DijagnozaId", "dbo.Dijagnozas");
            DropForeignKey("dbo.Terapijas", "PregledId", "dbo.Pregleds");
            DropForeignKey("dbo.Terapijas", "LijekId", "dbo.Lijeks");
            DropForeignKey("dbo.IzvrsenaUslugas", "UslugaId", "dbo.Uslugas");
            DropForeignKey("dbo.IzvrsenaUslugas", "PregledId", "dbo.Pregleds");
            DropForeignKey("dbo.Pregleds", "TerminId", "dbo.Termins");
            DropForeignKey("dbo.Termins", "PacijentId", "dbo.Pacijents");
            DropForeignKey("dbo.Pregleds", "StomatologId", "dbo.Stomatologs");
            DropForeignKey("dbo.Pregleds", "PacijentId", "dbo.Pacijents");
            DropForeignKey("dbo.Pacijents", "RacunId", "dbo.Racuns");
            DropForeignKey("dbo.Stomatologs", "Id", "dbo.Korisniks");
            DropForeignKey("dbo.Pacijents", "Id", "dbo.Korisniks");
            DropForeignKey("dbo.Osobljes", "Id", "dbo.Korisniks");
            DropForeignKey("dbo.Osobljes", "RacunId", "dbo.Racuns");
            DropIndex("dbo.UspostavljenaDijagnozas", new[] { "DijagnozaId" });
            DropIndex("dbo.UspostavljenaDijagnozas", new[] { "PregledId" });
            DropIndex("dbo.Terapijas", new[] { "LijekId" });
            DropIndex("dbo.Terapijas", new[] { "PregledId" });
            DropIndex("dbo.Termins", new[] { "PacijentId" });
            DropIndex("dbo.Stomatologs", new[] { "Id" });
            DropIndex("dbo.Osobljes", new[] { "RacunId" });
            DropIndex("dbo.Osobljes", new[] { "Id" });
            DropIndex("dbo.Pacijents", new[] { "RacunId" });
            DropIndex("dbo.Pacijents", new[] { "Id" });
            DropIndex("dbo.Pregleds", new[] { "TerminId" });
            DropIndex("dbo.Pregleds", new[] { "StomatologId" });
            DropIndex("dbo.Pregleds", new[] { "PacijentId" });
            DropIndex("dbo.IzvrsenaUslugas", new[] { "UslugaId" });
            DropIndex("dbo.IzvrsenaUslugas", new[] { "PregledId" });
            DropTable("dbo.UspostavljenaDijagnozas");
            DropTable("dbo.Terapijas");
            DropTable("dbo.Lijeks");
            DropTable("dbo.Uslugas");
            DropTable("dbo.Termins");
            DropTable("dbo.Stomatologs");
            DropTable("dbo.Racuns");
            DropTable("dbo.Osobljes");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Pacijents");
            DropTable("dbo.Pregleds");
            DropTable("dbo.IzvrsenaUslugas");
            DropTable("dbo.Dijagnozas");
        }
    }
}
