//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZavršniRad
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stomatolog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stomatolog()
        {
            this.Porukas = new HashSet<Poruka>();
            this.Pregleds = new HashSet<Pregled>();
        }
    
        public int Id { get; set; }
        public string Titula { get; set; }
        public string JMBG { get; set; }
    
        public virtual Korisnik Korisnik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Poruka> Porukas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pregled> Pregleds { get; set; }
    }
}
