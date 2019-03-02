using BO;

namespace ShootServ.Models
{
    public class UserInfoModel
    {
        public UserParams User { get; set; }

        public ShooterCategoryParams ShooterCategory { get; set; }

        public WeaponTypeParams Weapon { get; set; }

        public UserInfoModel(UserParams user)
        {
            User = user;
        }
    }
}