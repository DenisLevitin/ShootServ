using System;
using Newtonsoft.Json;

namespace ShootServ.Models.Cup
{
    /// <summary>
    /// Модель для упражнения
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class CompetitionModelParams 
    {
        /// <summary>
        /// ид. типа упражнения
        /// </summary>
        [JsonProperty]
        public int IdCompetitionType { get; set; }

        /// <summary>
        /// Ид. упражнения на соревновании
        /// </summary>
        [JsonProperty]
        public int IdCupCompetitionType { get; set; }

        /// <summary>
        /// Ид. типа оружия
        /// </summary>
        public int IdWeaponType { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// Время начала первой смены
        /// </summary>
        [JsonProperty]
        public DateTime TimeFirstShift { get; set; }

        /// <summary>
        /// Заявлено в соревновании
        /// </summary>
        public bool IsInCup { get; set; }

        public CompetitionModelParams()
        {
            IsInCup = false;
            IdCupCompetitionType = 0;
        }
    }
}