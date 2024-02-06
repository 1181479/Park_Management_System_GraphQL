using Park20.Backoffice.Core.Domain;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IParkLogRepository
    {
        Task<ParkLog?> CreateParkLog(ParkLog parkLog, DateTime startDatingTime);
        Task<bool> UpdateParkLogWithEndingTime(ParkLog requestDto);
        Task<bool> UpdateAvailableParkingSpots(string parkName, string licensePlate, bool isEntrance);
        Task<ParkLog> GetParkLog(string licensePlate);
    }
}
