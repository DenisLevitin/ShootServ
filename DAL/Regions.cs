//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Regions
    {
        public Regions()
        {
            this.ShootingRanges = new HashSet<ShootingRanges>();
        }
    
        public int IdRegion { get; set; }
        public string Name { get; set; }
        public string FederationAddress { get; set; }
        public string FederationTelefon { get; set; }
        public int IdCountry { get; set; }
    
        public virtual Countries Countries { get; set; }
        public virtual ICollection<ShootingRanges> ShootingRanges { get; set; }
    }
}
