namespace BO
{
    /// <summary>
    /// Разряд стрелка
    /// </summary>
    public class ShooterCategoryParams
    {
        /// <summary>
        /// Ид. разряда
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название разряда
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Порядковый номер разряда
        /// </summary>
        public int OrderSort { get; set; }
        
        /// <summary>
        /// Url на картинке
        /// </summary>
        public string PictureUrl { get; set; }
    }
}
