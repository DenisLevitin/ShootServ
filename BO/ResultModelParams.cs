namespace BO
{
    /// <summary>
    /// Результат соревнования
    /// </summary>
    public class ResultModelParams : ModelBase
    {
        /// <summary>
        /// Ид. стрелка
        /// </summary>
        public int IdShooter { get; set; }

        /// <summary>
        /// Ид. упражнения 
        /// </summary>
        public int IdCompetitionTypeCup { get; set; }

        /// <summary>
        /// Результат квалификации в очках
        /// </summary>
        public double ResultInPoints => Serie1.GetValueOrDefault() + 
                                       Serie2.GetValueOrDefault() + 
                                       Serie3.GetValueOrDefault() + 
                                       Serie4.GetValueOrDefault() + 
                                       Serie5.GetValueOrDefault() + 
                                       Serie6.GetValueOrDefault();


        public double? Serie1 { get; set; }

        public double? Serie2 { get; set; }

        public double? Serie3 { get; set; }

        public double? Serie4 { get; set; }

        public double? Serie5 { get; set; }

        public double? Serie6 { get; set; }

        public double? ResultOfFinal { get; set; }
    }
}
