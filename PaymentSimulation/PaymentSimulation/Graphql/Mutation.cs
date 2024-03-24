using Microsoft.AspNetCore.Mvc;
using PaymentSimulation.Models;
using PaymentSimulation.Services;

namespace PaymentSimulation.Graphql
{
    public class Mutation
    {
        private readonly IPaymentService _paymentService;

        public Mutation(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public Task<bool> ProcessPayment(PaymentRequest paymentRequest)
        {
            bool paymentSuccessful = _paymentService.ProcessPayment(paymentRequest);

            return Task.FromResult(paymentSuccessful);
        }

        public Task<bool> AddToken(string token)
        {
            bool paymentSuccessful = _paymentService.AddToken(token);

            return Task.FromResult(paymentSuccessful);
        }

        public Task<TokenResponse> GenerateToken(GenerateTokenRequest request)
        {
            TokenResponse token = _paymentService.GenerateToken(request);

            return Task.FromResult(token);
        }
    }
}
