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
    
    public partial class ShooterCategory
    {
        public ShooterCategory()
        {
            this.Shooters = new HashSet<Shooters>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Keychar { get; set; }
        public int OrderSort { get; set; }
        public string PictureUrl { get; set; }

        public virtual ICollection<Shooters> Shooters { get; set; }
    }
}
