namespace BO
{
    /// <summary>
    /// Тир
    /// </summary>
    public class ShootingRangeParams : ModelBase
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Информация о тире
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public int IdRegion { get; set; }

        /// <summary>
        /// Ид. юзера
        /// </summary>
        public int IdUser { get; set; }
    }
}
