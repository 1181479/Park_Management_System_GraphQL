using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IParkRepository
    {
        Task<Park> GetParkByName(string name);
        Task<List<ParkingSpot>> GetParkingSpotsByParkName(string parkName);
        Task<List<ParkingSpot>> GetParkingSpotsAvailableByParkName(string parkName);
        Task<IEnumerable<Park>> GetAllParks();
        Task<bool> UpdateParkPriceTable(string ParkName, double NightFee, PriceTable table);
        Task<List<string>> GetParkNames();
        Task<bool> UpdateParkingSpotStatus(bool status, int parkingSpotId);


    }
}
