namespace BO
{
    /// <summary>
    /// Тип оружия стрелка ( пистолет или винтовка(лучше название сменить))
    /// </summary>
    public partial class WeaponTypeParams : ModelBase
    {
        /// <summary>
        /// Название типа
        /// </summary>
        public string Name { get; set; }

        public string Keychar { get; set; }

        public string PictureUrl { get; set; }
    }
}
