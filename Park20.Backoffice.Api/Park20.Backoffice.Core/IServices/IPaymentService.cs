using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;

namespace Park20.Backoffice.Core.IServices
{
    public interface IPaymentService
    {
        Task<PaymentMethodResultDto?> AddPaymentMethodToUser(CreatePaymentMethodRequestDto createPaymentMethodRequestDto);
        Task<IEnumerable<PaymentMethodResultDto>> GetPaymentMethodListFromUser(string username);
        Task<HibridPayment> MakePayment(string licensePlate, decimal totalCost);
        Task<decimal> CalculateCost(string parkName, string licensePlate);
        public void PrintMetrics();
    }
}
