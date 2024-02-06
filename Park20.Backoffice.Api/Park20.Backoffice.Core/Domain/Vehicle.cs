using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain
{
    public class Vehicle
    {
        public string LicensePlate { get; set; }
        public string Brand {  get; set; }
        public string Model { get; set; }
        public VehicleType Type { get; set; }
    }
}
