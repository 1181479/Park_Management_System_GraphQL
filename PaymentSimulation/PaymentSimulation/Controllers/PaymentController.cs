using Microsoft.AspNetCore.Mvc;
using PaymentSimulation.Models;
using PaymentSimulation.Services;

namespace PaymentSimulation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            object paymentSuccessful = _paymentService.ProcessPayment(paymentRequest);

            return Ok(paymentSuccessful);
        }
        
        [HttpPost("Payment/token")]
        public IActionResult AddToken([FromQuery] string token)
        {
            bool paymentSuccessful = _paymentService.AddToken(token);

            return Ok(paymentSuccessful);
        }

        [HttpPost("GenerateToken")]
        public IActionResult GenerateToken([FromBody] GenerateTokenRequest request)
        {
            TokenResponse token = _paymentService.GenerateToken(request);

            return Ok(token);
        }
    }
}
