using System.Collections.Generic;
using System.Web.Mvc;

namespace ShootServ.Models.ShootingClub
{
    public class ShooterClubPageModel
    {
        public ShooterClubPageModel()
        {
            SavingShooterClub = new ShooterClubModelParams();
            Countries = new List<SelectListItem>();
            Regions = new List<SelectListItem>();
            ShootingRanges = new List<SelectListItem>();
        }
        
        public ShooterClubModelParams SavingShooterClub { get; set; }
        
        public IReadOnlyCollection<SelectListItem> Countries { get; set; }
        
        public IReadOnlyCollection<SelectListItem> Regions { get; set; }
        
        public IReadOnlyCollection<SelectListItem> ShootingRanges { get; set; }
    }
}