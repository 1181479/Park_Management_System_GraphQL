using Park20.LicensePlateReader.Api.Models.Dtos;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace Park20.LicensePlateReader.Api.Services
{
    public class VehicleCheckService : IVehicleCheckService
    {
        public async Task<bool> VehicleChecks(string licence, string parkName)
        {
            if (await CheckVehicleExists(licence))
            {
                if (await CheckVehicleType(licence, parkName))
                {
                    return await CheckParkOccupancy(licence, parkName);
                }
            }
            return false;
        }
        private async Task<bool> CheckParkOccupancy(string licence, string parkName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5000/api/Park/GetAvailableSpace?LicencePlate={licence}&ParkName={parkName}";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if ((int)response.StatusCode != 200 || !bool.Parse(responseContent))
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
        private async Task<bool> CheckVehicleExists(string licence)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string escapedLicence = Uri.EscapeDataString(licence);

                    string apiUrl = $"http://localhost:5000/api/Vehicle/GetVehicleByLicencePlate?license={licence}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return bool.Parse(responseContent);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                    return false;
                }
            }
        }
        private async Task<bool> CheckVehicleType(string licence, string parkName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string escapedLicence = Uri.EscapeDataString(licence);
                    string escapedParkName = Uri.EscapeDataString(parkName);

                    string apiUrl = $"http://localhost:5000/api/Park/GetVehicleTypeAvailable?LicencePlate={escapedLicence}&ParkName={escapedParkName}";

                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();

                    return bool.Parse(responseContent);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                    return false;
                }
            }
        }

    }
}
