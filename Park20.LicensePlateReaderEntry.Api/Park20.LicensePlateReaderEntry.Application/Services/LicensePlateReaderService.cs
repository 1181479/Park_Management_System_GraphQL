using Park20.LicensePlateReader.Api.Models.Dtos;
using Park20.LicensePlateReader.Api.Services;
using Park20.LicensePlateReader.Core.IServices;
using System.Net.Http.Json;

namespace Park20.LicensePlateReader.Api.Services
{
    public class LicensePlateReaderService : ILicensePlateReaderService
    {
        private readonly IVehicleCheckService vehicleCheckService;
        private static string defaultLicensePlate = "bb-00-00";
        private static string defaultPark = "Parque de Estacionamento Trindade";


        public LicensePlateReaderService(IVehicleCheckService vehicleCheckService)
        {

            this.vehicleCheckService = vehicleCheckService;

        }

        public async Task<bool> ReadLicensePlateEntrance()
        {
            if (await vehicleCheckService.VehicleChecks(defaultLicensePlate, defaultPark))
            {
                await OpenBarrier();
                await ShowWelcomeMessage();
                Thread.Sleep(4000);
                await ParkCar(defaultLicensePlate, defaultPark);
                await CloseBarrier();
                await ShowGeneralMessage();
                return true;
            }
            return false;
        }

        public bool UpdateLicensePlate(string newLicensePlate)
        {
            defaultLicensePlate = newLicensePlate;
            return true;
        }

        private async Task<bool> OpenBarrier()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5002/api/Barrier/OpenBarrier";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if ((int)response.StatusCode != 200)
                    {
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                }
                return true;
            }
        }

        private async Task<bool> CloseBarrier()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5002/api/Barrier/CloseBarrier";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if ((int)response.StatusCode != 200)
                    {
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                }
                return true;
            }
        }



        private async Task<bool> ParkCar(string licensePlate, string parkName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5000/api/Vehicle/ParkCar";

                    ParkCheckRequestDto requestDto = new ParkCheckRequestDto
                    {
                        LicensePlate = licensePlate,
                        ParkName = parkName,
                        IsEntrance = true
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, requestDto);

                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                }
                return true;
            }
        }
        private async Task<bool> ShowWelcomeMessage()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5003/Display/ShowWelcomeMessage";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if ((int)response.StatusCode != 200)
                    {
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                }
                return true;
            }
        }
        private async Task<bool> ShowGeneralMessage()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5003/Display/ShowGeneralMessage";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if ((int)response.StatusCode != 200)
                    {
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                }
                return true;
            }
        }
    }
}
