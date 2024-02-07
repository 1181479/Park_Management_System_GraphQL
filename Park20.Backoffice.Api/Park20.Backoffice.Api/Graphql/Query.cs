using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Park;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Api.Graphql
{
    public class Query
    {
        private readonly IUserService _userService;
        private readonly IVehicleService _vehicleService;
        private readonly IPaymentService _paymentService;
        private readonly IParkyWalletService _parkyWalletService;
        private readonly IParkService _parkService;
        private readonly IDashboardService _dashboardService;

        public Query(IUserService userService, IVehicleService vehicleService, IPaymentService paymentService, IParkyWalletService parkyWalletService, IParkService parkService, IDashboardService dashboardService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _paymentService = paymentService;
            _parkyWalletService = parkyWalletService;
            _parkService = parkService;
            _dashboardService = dashboardService;
        }

        public async Task<bool> CheckIfUserIsRegistered(string username, string password)
        {
            return await _userService.CheckIfUserIsRegistered(username, password);
        }

        public async Task<CreateCustomerResultDto> GetUserByUsername(string username)
        {
            return await _userService.GetUserByUsername(username);
        }

        public async Task<List<CreateCustomerResultDto>> GetUsersBeforeDate(DateTime time)
        {
            return await _userService.GetUsersBeforeDate(time);
        }

        public async Task<VehicleResultDto?> GetVehicleByLicencePlate(string license)
        {
            return await _vehicleService.GetVehicle(license);
        }

        public async Task<IEnumerable<VehicleResultDto?>> GetVehicleListFromUser(string username)
        {
            return await _vehicleService.GetVehicleListFromUser(username);
        }

        public async Task<IEnumerable<PaymentMethodResultDto>> GetPaymentMethodListFromUser(string username)
        {
            return await _paymentService.GetPaymentMethodListFromUser(username);
        }

        public async Task<int> GetParkingPerHourValue()
        {
            return await _parkyWalletService.GetParkingValueAsync();
        }

        public async Task<int> GetRegistryValue()
        {
            return await _parkyWalletService.GetRegestryValue();
        }

        public async Task<int> GetBulkValue()
        {
            return await _parkyWalletService.GetBulkValue();
        }

        public async Task<ParkyWalletDto> GetParkyWalletByUsername(string username)
        {
            return await _parkyWalletService.GetParkyWalletByUsername(username);
        }

        public async Task<bool> GetAvailableSpace(ParkCheckRequestDto parkCheckRequestDto)
        {
            if (parkCheckRequestDto == null)
            {
                return false;
            }
            var vehicle = await _vehicleService.GetVehicle(parkCheckRequestDto.LicencePlate);
            if (vehicle == null)
                return false;
            var availableSpace = await _parkService.GetAvailableSpace(vehicle.Type, parkCheckRequestDto.ParkName);

            if (availableSpace == null || !availableSpace.HasValue)
            {
                return false;
            }
            return availableSpace.Value;
        }

        public async Task<bool> GetVehicleTypeAvailable(ParkCheckRequestDto parkCheckRequestDto)
        {
            if (parkCheckRequestDto == null)
            {
                return false;
            }
            var vehicle = await _vehicleService.GetVehicle(parkCheckRequestDto.LicencePlate);
            if (vehicle == null)
                return false;

            var vehicleTypeAvailable = await _parkService.GetVehicleTypeAvailable(vehicle.Type, parkCheckRequestDto.ParkName);

            if (vehicleTypeAvailable == null || !vehicleTypeAvailable.HasValue)
            {
                return false;
            }
            return vehicleTypeAvailable.Value;
        }

        [HttpGet]
        public async Task<IEnumerable<Park>> GetAllParks()
        {
            return await _parkService.GetAllParks();
        }

        public async Task<ParkingSpotCountDto> GetParkingSpotsCount(string parkName)
        {
            return await _parkService.GetNumberParkingSpots(parkName);
        }

        public async Task<IEnumerable<ParkDistanceResultDto>> GetAllParksByDistance(double latitude, double longitude)
        {
            return await _parkService.GetAllParksWithDistance(latitude, longitude);
        }

        public async Task<List<string>> GetParkNames()
        {
            return await _parkService.GetParkNames();
        }

        public async Task<List<ParkingSpot>> GetParkingSpots(string parkName)
        {
            return await _parkService.GetParkingSpots(parkName);
        }

        public async Task<List<DashboardElements>> CreateDashBoardMostSpenders(CreateDashboardUsageParkyCoinsRequestDto createDashboardUsage)
        {
            return await _dashboardService.GetUsersWithMostParkyCoinsSpent(createDashboardUsage.parkName, createDashboardUsage.initialDate, createDashboardUsage.endDate, createDashboardUsage.vehicleType, createDashboardUsage.totalMinutes);
        }

        public async Task<List<DashboardElements>> CreateDashBoardWorstSpenders(CreateDashboardUsageParkyCoinsRequestDto createDashboardUsage)
        {
            return await _dashboardService.GetUsersWithLessParkyCoinsSpent(createDashboardUsage.parkName, createDashboardUsage.initialDate, createDashboardUsage.endDate, createDashboardUsage.vehicleType, createDashboardUsage.totalMinutes);
        }

        public async Task<List<DashboardElements>> CreateDashBoardMidSpenders(CreateDashboardUsageParkyCoinsRequestDto createDashboardUsage)
        {
            return await _dashboardService.GetUsersWithMidParkyCoinsSpent(createDashboardUsage.parkName, createDashboardUsage.initialDate, createDashboardUsage.endDate, createDashboardUsage.vehicleType, createDashboardUsage.totalMinutes);
        }
    }
}
