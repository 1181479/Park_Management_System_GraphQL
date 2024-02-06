using PaymentSimulation.Models;

namespace PaymentSimulation.Services
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentRequest paymentRequest);//, out string receipt, out string confirmation);
        bool AddToken(string token);
        TokenResponse GenerateToken(GenerateTokenRequest request);
    }
}
