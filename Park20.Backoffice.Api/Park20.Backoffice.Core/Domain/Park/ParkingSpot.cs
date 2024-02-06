using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain.Park
{
    public class ParkingSpot
    {
        public int ParkingSpotId { get; set; }
        public VehicleType VehicleType { get; set; }
        public bool Status { get; set; }
        public int FloorNumber { get; set; }
    }
}
