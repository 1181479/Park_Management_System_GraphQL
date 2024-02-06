using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class UpdateParkingSpotStatusRequestDto
    {
        public bool Status { get; set; }
        public int ParkingSpotId { get; set; }
    }
}
