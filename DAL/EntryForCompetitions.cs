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
    
    public partial class EntryForCompetitions
    {
        public int Id { get; set; }
        public int IdShooter { get; set; }
        public int IdCupCompetitionType { get; set; }
        public System.DateTime DateCreate { get; set; }
        public int IdEntryStatus { get; set; }
    
        public virtual CupCompetitionType CupCompetitionType { get; set; }
        public virtual EntryStatus EntryStatus { get; set; }
        public virtual Shooters Shooters { get; set; }
    }
}
