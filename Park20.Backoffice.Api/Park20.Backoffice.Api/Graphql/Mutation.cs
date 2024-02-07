using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Api.Graphql
{
    public class Mutation
    {
        private readonly IUserService _userService;
        private readonly IVehicleService _vehicleService;
        private readonly IParkLogService _parkLogService;
        private readonly IPaymentService _paymentService;
        private readonly IParkyWalletService _parkyWalletService;
        private readonly IParkService _parkService;

        public Mutation(IUserService userService, IVehicleService vehicleService, IParkLogService parkLogService, IPaymentService paymentService, IParkyWalletService parkyWalletService, IParkService parkService)
        {
            _userService = userService;
            _vehicleService = vehicleService;
            _parkLogService = parkLogService;
            _paymentService = paymentService;
            _parkyWalletService = parkyWalletService;
            _parkService = parkService;
        }

        public async Task<CreateCustomerResultDto> AddCustomer(CreateCustomerRequestDto createCustomerRequestDto)
        {
            if (await _userService.CheckIfEmailExists(createCustomerRequestDto.Email))
            {
                return new CreateCustomerResultDto("", "", "");
            }
            return await _userService.AddCustomer(createCustomerRequestDto);
        }

        public async Task<VehicleResultDto?> AddVehicle(CreateVehicleRequestDto createVehicleRequestDto)
        {
            return await _vehicleService.AddVehicleToUser(createVehicleRequestDto);
        }

        public async Task<bool> ParkCar(ParkingSpotsUpdateRequestDto requestDto)
        {
            await _parkLogService.StartingCountingTimeOperation(requestDto.LicensePlate, requestDto.ParkName);
            return await _parkLogService.UpdateAvailableParkingSpots(requestDto.ParkName, requestDto.LicensePlate, requestDto.IsEntrance);
        }

        public async Task<HibridPayment> LeavePark(ParkingSpotsUpdateRequestDto requestDto)
        {
            await _parkLogService.StopCountingTimeOperation(requestDto.LicensePlate, requestDto.ParkName);

            var totalCost = await _paymentService.CalculateCost(requestDto.ParkName, requestDto.LicensePlate);

            var payment = await _paymentService.MakePayment(requestDto.LicensePlate, totalCost);

            await _parkLogService.UpdateAvailableParkingSpots(requestDto.ParkName, requestDto.LicensePlate, requestDto.IsEntrance);
            return payment;
        }

        public async Task<PaymentMethodResultDto?> AddPaymentMethod(CreatePaymentMethodRequestDto createPaymentMethodRequestDto)
        {
            return await _paymentService.AddPaymentMethodToUser(createPaymentMethodRequestDto);
        }

        public async Task<bool> BulkParky(List<string> customerUsernames)
        {
            return await _parkyWalletService.BulkParky(customerUsernames);
        }

        public async Task<bool> UpdateBulkValue(int value)
        {
            return await _parkyWalletService.UpdateBulkValue(value);
        }

        public async Task<bool> UpdateNewCustomerValue(int value)
        {
            return await _parkyWalletService.UpdateNewCustomerValue(value);
        }

        public async Task<bool> UpdateParkingValue(int value)
        {
            return await _parkyWalletService.UpdateParkingValue(value);
        }

        public async Task<bool?> UpdatePriceTable(PriceTableDto updatePriceTable)
        {
            return await _parkService.UpdatePriceTable(updatePriceTable);
        }

        public async Task<bool> UpdateParkingSpotStatus(UpdateParkingSpotStatusRequestDto updateParkingSpotStatusRequestDto)
        {
            return await _parkService.UpdateParkingSpotStatus(updateParkingSpotStatusRequestDto.Status, updateParkingSpotStatusRequestDto.ParkingSpotId);
        }
    }
}
