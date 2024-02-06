using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IServices
{
    public interface IVehicleService
    {

        Task<VehicleResultDto?> AddVehicleToUser(CreateVehicleRequestDto createVehicleRequestDto);
        Task<VehicleResultDto?> GetVehicle(string licence);
        Task<IEnumerable<VehicleResultDto>> GetVehicleListFromUser(string username);
    }
}
