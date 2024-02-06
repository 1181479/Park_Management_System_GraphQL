using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class ParkingSpotsUpdateRequestDto
    {
        public string ParkName {  get; set; }
        public string LicensePlate { get; set; }
        public bool IsEntrance { get; set; }    
    }
}
