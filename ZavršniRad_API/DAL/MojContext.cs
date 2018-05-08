using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ZavršniRad_API.DAL
{
        public class MojContext : DbContext
        {
            public MojContext() : base("MyConnection")
            {

            }



            public DbSet<Pacijent> Pacijent { set; get; }
            public DbSet<Dijagnoza> Dijagnoza { set; get; }
            public DbSet<IzvrsenaUsluga> IzvrsenaUsluga { set; get; }
            public DbSet<Korisnik> Korisnik { set; get; }
            public DbSet<Lijek> Lijek { set; get; }
            public DbSet<Materijal> Materijal { set; get; }
        public DbSet<PotroseniMaterijal> PotroseniMaterijal { set; get; }

        public DbSet<Osoblje> Osoblje { set; get; }
            public DbSet<Pregled> Pregled { set; get; }
            public DbSet<Racun> Racun { set; get; }
            public DbSet<Stomatolog> Stomatolog { set; get; }
            public DbSet<Terapija> Terapija { set; get; }
            public DbSet<Termin> Termin { set; get; }
            public DbSet<Usluga> Usluga { set; get; }
        public DbSet<Zub> Zub { set; get; }
        public DbSet<Poruka> Poruka { set; get; }
        public DbSet<Ocjena> Ocjena { set; get; }



        public DbSet<UspostavljenaDijagnoza> UspostavljenaDijagnoza { set; get; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

                modelBuilder.Entity<Korisnik>().HasOptional(x => x.Pacijent).WithRequired(x => x.Korisnik);
               modelBuilder.Entity<Korisnik>().HasOptional(x => x.Stomatolog).WithRequired(x => x.Korisnik);
                modelBuilder.Entity<Korisnik>().HasOptional(x => x.Osoblje).WithRequired(x => x.Korisnik);

            }

        }
    }
