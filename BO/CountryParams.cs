namespace BO
{
    /// <summary>
    /// Страна
    /// </summary>
    public class CountryParams : ModelBase
    {
        /// <summary>
        /// Код страны
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Название страны
        /// </summary>
        public string CountryName { get; set; }
    }
}
