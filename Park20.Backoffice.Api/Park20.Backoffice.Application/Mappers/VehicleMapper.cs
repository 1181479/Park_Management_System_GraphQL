using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;


namespace Park20.Backoffice.Application.Mappers
{
    public static class VehicleMapper
    {
        public static VehicleResultDto ToVehicleDto(Vehicle Vehicle)
        {
            return new VehicleResultDto(Vehicle.LicensePlate, Vehicle.Brand, Vehicle.Model, Enum.GetName(Vehicle.Type)!);
        }

        public static Vehicle ToVehicleDomain(CreateVehicleRequestDto vehicleDto)
        {
            return new Vehicle
            {
                LicensePlate = vehicleDto.LicensePlate,
                Model = vehicleDto.Model,
                Brand = vehicleDto.Brand,
                Type = (VehicleType)Enum.Parse(typeof(VehicleType), vehicleDto.Type)
            };
          
        }
    }
}
