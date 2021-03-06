//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZavršniRad_API
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pregled
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pregled()
        {
            this.IzvrsenaUslugas = new HashSet<IzvrsenaUsluga>();
            this.Terapijas = new HashSet<Terapija>();
            this.UspostavljenaDijagnozas = new HashSet<UspostavljenaDijagnoza>();
            this.Racuns = new HashSet<Racun>();
        }
    
        public int Id { get; set; }
        public System.DateTime DatumPregleda { get; set; }
        public System.DateTime VrijemePregleda { get; set; }
        public int PacijentId { get; set; }
        public int StomatologId { get; set; }
        public int TerminId { get; set; }
        public bool IsObavljen { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IzvrsenaUsluga> IzvrsenaUslugas { get; set; }
        public virtual Pacijent Pacijent { get; set; }
        public virtual Stomatolog Stomatolog { get; set; }
        public virtual Termin Termin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Terapija> Terapijas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UspostavljenaDijagnoza> UspostavljenaDijagnozas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Racun> Racuns { get; set; }
    }
}
