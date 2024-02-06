using Park20.Backoffice.Core.Dtos.Requests;

namespace Park20.Backoffice.Core.IServices
{
    public interface IParkLogService
    {
        Task StartingCountingTimeOperation(string licensePlate, string parkName);
        Task StopCountingTimeOperation(string licensePlate, string parkName);
        Task<bool> UpdateAvailableParkingSpots(string parkName, string licensePlate, bool isEntrance);
    }
}
