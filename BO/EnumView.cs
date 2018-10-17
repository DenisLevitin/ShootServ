namespace BO
{
    /// <summary>
    /// Представление значения Enum
    /// </summary>
    public class EnumView
    {
        /// <summary>
        /// Id значения enum
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// строковое значение представления enum
        /// </summary>
        public string EnumValue { get; private set; }
        
        /// <summary>
        /// аттрибут description
        /// </summary>
        public string EnumDescription { get; private set; }

        public EnumView(int id, string enumValue, string enumDescription)
        {
            Id = id;
            EnumValue = enumValue;
            EnumDescription = enumDescription;
        }
    }
}