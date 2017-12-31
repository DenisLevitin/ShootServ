using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Детализация стрелкового клуба
    /// </summary>
    public class ShooterClubDetalisationParams
    {
        public ShooterClubParams Club { get; set; }

        public string RegionName { get; set; }
    }
}
