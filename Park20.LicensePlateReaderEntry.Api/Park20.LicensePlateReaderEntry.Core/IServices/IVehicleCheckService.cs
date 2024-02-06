namespace Park20.LicensePlateReader.Api.Services
{
    public interface IVehicleCheckService
    {
        Task<bool> VehicleChecks(string licence, string parkName);
    }
}
