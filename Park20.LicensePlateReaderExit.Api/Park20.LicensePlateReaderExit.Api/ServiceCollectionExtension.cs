
using Park20.LicensePlateReader.Api.Services;
using Park20.LicensePlateReader.Application.Services;
using Park20.LicensePlateReader.Core.IServices;

namespace Park20.LicensePlateReader.Api
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILicensePlateReaderService, LicensePlateReaderService>().AddTransient<IVehicleCheckService,VehicleCheckService>();
        }

    }
}
