using Park20.LicensePlateReader.Api.Models.Dtos;
using Park20.LicensePlateReader.Api.Services;
using Park20.LicensePlateReader.Core.IServices;
using Park20.LicensePlateReaderExit.Core.Models.Dtos;
using System.Net.Http.Json;

namespace Park20.LicensePlateReader.Application.Services
{
    public class LicensePlateReaderService : ILicensePlateReaderService
    {
        private static string defaultLicensePlate = "bb-00-00";
        private static string defaultPark = "Parque de Estacionamento Trindade";


        public LicensePlateReaderService()
        {

        }
      
        public async Task<bool> ReadLicensePlateExit()
        {
            var payment =await LeavePark(defaultLicensePlate, defaultPark);
            await OpenBarrier();
            await ShowGoodbyeMessage(payment.parkyCoinsAmount, payment.otherPaymentMethodAmount, payment.totalCost);
            Thread.Sleep(4000);
            await CloseBarrier();
            await ShowGeneralMessage();
            return true;

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
                    string apiUrl = $"http://localhost:6002/api/Barrier/OpenBarrier";
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
                    string apiUrl = $"http://localhost:6002/api/Barrier/CloseBarrier";
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


        private async Task<HybridPaymentDto?> LeavePark(string licensePlate, string parkName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:5000/api/Vehicle/LeavePark";

                    ParkCheckRequestDto requestDto = new ParkCheckRequestDto
                    {
                        LicensePlate = licensePlate,
                        ParkName = parkName,
                        IsEntrance = false
                    };

                    HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, requestDto);

                    if (response.IsSuccessStatusCode)
                    {
                        var payment = await response.Content.ReadFromJsonAsync<HybridPaymentDto>();
                        return payment;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Erro na requisição: {e.Message}");
                    return null;
                }
            }
        }
        private async Task<bool> ShowGeneralMessage()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:6003/Display/ShowGeneralMessage";
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
        private async Task<bool> ShowGoodbyeMessage(decimal? parkyCoinsAmount, decimal? otherPaymentMethodAmount, decimal? totalCost)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"http://localhost:6003/Display/ShowGoodbyeMessage?parkyCoinsSpent={parkyCoinsAmount}&otherPaymentMethodSpent={otherPaymentMethodAmount}&totalCost={totalCost}";

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
