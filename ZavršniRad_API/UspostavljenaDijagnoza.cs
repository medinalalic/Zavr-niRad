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
    
    public partial class UspostavljenaDijagnoza
    {
        public int Id { get; set; }
        public int Intenzitet { get; set; }
        public string Napomena { get; set; }
        public int PregledId { get; set; }
        public int DijagnozaId { get; set; }
        public int ZubId { get; set; }
    
        public virtual Dijagnoza Dijagnoza { get; set; }
        public virtual Pregled Pregled { get; set; }
        public virtual Zub Zub { get; set; }
    }
}
