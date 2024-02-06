using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class CreateDashboardUsageParkyCoinsRequestDto
    {
        public string? parkName { get; set; }
        public DateTime? initialDate { get; set; }
        public DateTime? endDate { get; set; }
        public string? vehicleType { get; set; }      
        public int?  totalMinutes { get; set; }
    }
}
