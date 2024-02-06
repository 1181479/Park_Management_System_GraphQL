using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public record CreateVehicleRequestDto(string LicensePlate, string Brand, string Model, string Type, string Username);
}
