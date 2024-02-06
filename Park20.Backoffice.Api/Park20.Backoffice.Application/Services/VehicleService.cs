using Park20.Backoffice.Application.Mappers;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;


namespace Park20.Backoffice.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;


        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleResultDto?> AddVehicleToUser(CreateVehicleRequestDto createVehicleRequestDto)
        {
            var vehicle = VehicleMapper.ToVehicleDomain(createVehicleRequestDto);
            var result = await _vehicleRepository.AddVehicle(vehicle, createVehicleRequestDto.Username);
            if (result != null)
            {
                return VehicleMapper.ToVehicleDto(result);
            }
            return default;
        }

        public async Task<VehicleResultDto?> GetVehicle(string licence)
        {
            var result = await _vehicleRepository.GetVehicle(licence);
            if (result != null)
            {
                return VehicleMapper.ToVehicleDto(result);
            }
            return default;
        }
        
        public async Task<IEnumerable<VehicleResultDto>> GetVehicleListFromUser(string username)
        {
            var result = await _vehicleRepository.GetAllFromUser(username);
            List<VehicleResultDto> listVehicles = new List<VehicleResultDto>();
            foreach (var item in result)
            {
                listVehicles.Add(VehicleMapper.ToVehicleDto(item));
            }
            return listVehicles;
        }
    }
}
