using Park20.Backoffice.Application.Mappers;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class ParkLogService(IParkLogRepository parkLogRepository) : IParkLogService
    {
        private readonly IParkLogRepository _parkLogRepository = parkLogRepository;

        public async Task StartingCountingTimeOperation(string licensePlate, string parkName)
        {
            var startingTime = DateTime.UtcNow;

            await CreateParkLog(licensePlate, parkName, startingTime);
        }

        private async Task CreateParkLog(string licensePlate, string parkName, DateTime startingTime)
        {
            await _parkLogRepository.CreateParkLog(ParkLogMapper.ToParkLogDomain(licensePlate, parkName), startingTime);
        }

        public async Task StopCountingTimeOperation(string licensePlate, string parkName)
        {
            var endingTime = DateTime.UtcNow;
            await ManageParkLog(licensePlate, parkName, endingTime);
        }

        private async Task ManageParkLog(string licensePlate, string parkName, DateTime endingTime)
        {
            var parkLog = ParkLogMapper.ToParkLogDomain(licensePlate, parkName);

            parkLog.EndTime = endingTime;

            var isUpdated = await _parkLogRepository.UpdateParkLogWithEndingTime(parkLog);

        }

        public async Task<bool> UpdateAvailableParkingSpots(string parkName, string licensePlate, bool isEntrance)
        {
            return await _parkLogRepository.UpdateAvailableParkingSpots(parkName, licensePlate, isEntrance);
        }

    }
}
