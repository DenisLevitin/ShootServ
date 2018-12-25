namespace BO
{
    /// <summary>
    /// Тир
    /// </summary>
    public class ShootingRangeListItem
    {
        /// <summary>
        /// Ид.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Город
        /// </summary>
        public string Town { get; set; }
        
        /// <summary>
        /// Местный телефон
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Описание
        /// </summary>
        public string Info { get; set; }
        
        /// <summary>
        /// Ид. региона
        /// </summary>
        public int IdRegion { get; set; }
        
        /// <summary>
        /// Ид. создравшего тир юзера
        /// </summary>
        public int IdUser { get; set; }
        
        public string RegionName { get; set; }
        
        /// <summary>
        /// Создатель тира
        /// </summary>
        public UserParams Creator { get; set; }

        /// <summary>
        /// Название тира
        /// </summary>
        public string Name { get; set; }
    }
}