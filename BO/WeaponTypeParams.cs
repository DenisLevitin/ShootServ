namespace BO
{
    /// <summary>
    /// Тип оружия стрелка ( пистолет или винтовка(лучше название сменить))
    /// </summary>
    public class WeaponTypeParams
    {
        /// <summary>
        /// Ид. типа оружия
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название типа
        /// </summary>
        public string Name { get; set; }

        public string Keychar { get; set; }

        public enum WeaponTypeEnum
        {
            Rifle = 1,
            Pistol = 2,
            RifleMovingTarget = 3
        }
    }
}
