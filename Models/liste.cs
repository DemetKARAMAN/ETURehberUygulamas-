//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace etu_rehber.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class liste
    {
        public int liste_id { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string tel { get; set; }
        public string mail { get; set; }
        public string gorevi { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public string entity { get; set; }
        public int admin_id { get; set; }
        public int fak_id { get; set; }
        public int birim_id { get; set; }
        public int bolum_id { get; set; }
        public Nullable<int> sifre { get; set; }
    
        public virtual admin admin { get; set; }
        public virtual birim birim { get; set; }
        public virtual bolum bolum { get; set; }
        public virtual fakulte fakulte { get; set; }
    }
}
