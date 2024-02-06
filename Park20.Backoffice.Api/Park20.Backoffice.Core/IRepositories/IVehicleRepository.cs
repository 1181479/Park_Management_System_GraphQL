using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> AddVehicle(Vehicle vehicle, string username);
        Task<Vehicle?> GetVehicle(string licence);
        Task<IEnumerable<Vehicle>> GetAllFromUser(string username);
    }
}
