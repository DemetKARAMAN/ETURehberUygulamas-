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
    
    public partial class yetki
    {
        public int yetki_id { get; set; }
        public int admin_id { get; set; }
        public int fak_id { get; set; }
        public int birim_id { get; set; }
    
        public virtual admin admin { get; set; }
        public virtual birim birim { get; set; }
        public virtual fakulte fakulte { get; set; }
    }
}
