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
    
    public partial class Terapija
    {
        public int Id { get; set; }
        public int Vrsta { get; set; }
        public int Količina { get; set; }
        public int PregledId { get; set; }
        public int LijekId { get; set; }
    
        public virtual Lijek Lijek { get; set; }
        public virtual Pregled Pregled { get; set; }
    }
}
