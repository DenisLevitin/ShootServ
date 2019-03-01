using BO;
using System.Collections.Generic;

namespace ShootServ.Models
{
    public class UserInfoModel
    {
        public UserParams User { get; set; }

        public IReadOnlyCollection<ShooterCategoryParams> ShooterCategories { get; set; }

        public ShooterCategoryParams ShooterCategory { get; set; }

        public int? IdWeaponType { get; set; }

        public UserInfoModel()
        {
            ShooterCategories = new List<ShooterCategoryParams>();
        }

        public UserInfoModel(UserParams user) : this()
        {
            User = user;
        }
    }
}