using Microsoft.AspNetCore.Mvc;
using PaymentSimulation.Services;

namespace PaymentSimulation.Graphql
{
    public class Query
    {
        private readonly IPaymentService _paymentService;

        public Query(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public string HelloWorld()
        {
            return "Hello, from GraphQL!";
        }
    }
}
