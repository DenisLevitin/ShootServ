using System;

namespace BO
{
    /// <summary>
    /// Соревнование
    /// </summary>
    public class CupParams : ModelBase
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ид. типа кубка
        /// </summary>
        public int IdCupType { get; set; }

        /// <summary>
        /// Дата начала кубка
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Дата окончания кубка
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Тир
        /// </summary>
        public int IdShootingRange { get; set; }

        /// <summary>
        /// Положение о соревновании
        /// </summary>
        public byte[] Document { get; set; }

        /// <summary>
        /// Ид. юзера, создавшего соревнование
        /// </summary>
        public int IdUser { get; set; }

        /// <summary>
        /// Дата создания соревнования
        /// </summary>
        public DateTime DateCreate { get; set; }
    }
}
