using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Results
{
    public class CreateParkLogResult
    {
        public string LicencePlate { get; set; }
        public string ParkName { get; set; }
        public DateTime startingTime { get; set; }

    }
}
