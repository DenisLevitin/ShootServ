using System.ComponentModel;

namespace BO
{
    /// <summary>
    /// Вид соревнования, масштаб
    /// </summary>
    public enum CupTypeParams
    {
        /// <summary>
        /// Городские
        /// </summary>
        [Description("Городские")]
        City = 1,
        
        /// <summary>
        /// Региональные
        /// </summary>
        [Description("Региональные")]
        Region = 2,
        
        /// <summary>
        /// Всероссийские
        /// </summary>
        [Description("Всероссийские")]
        Country = 3,
        
        /// <summary>
        /// Мировые
        /// </summary>
        [Description("Мировые")]
        World = 4
    }
}