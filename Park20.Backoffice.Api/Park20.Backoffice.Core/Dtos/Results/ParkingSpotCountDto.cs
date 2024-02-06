using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Results
{
    public class ParkingSpotCountDto
    {
        public int MotocycleCount { get; set; }
        public int GPLCount { get; set; }
        public int ElectricCount { get; set; }
        public int AutomobileCount { get; set; }
    }
}
