using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Park20.Backoffice.Application.Services;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController (IPaymentService paymentService) : ControllerBase
    {

        private readonly IPaymentService _paymentService = paymentService;
  

        [HttpPost]
        public async Task<IActionResult> AddPaymentMethod([FromBody] CreatePaymentMethodRequestDto createPaymentMethodRequestDto)
        {
            var paymentMethod = await _paymentService.AddPaymentMethodToUser(createPaymentMethodRequestDto);

            return Ok(paymentMethod);
        }

        [HttpGet("GetAllFromUser/{username}")]
        public async Task<IActionResult> GetPaymentMethodListFromUser(string username)
        {
            var paymentMethodList = await _paymentService.GetPaymentMethodListFromUser(username);
            return Ok(paymentMethodList);
        }
        
        [HttpGet("PrintMetrics")]
        public IActionResult PrintMetrics()
        {
            _paymentService.PrintMetrics();
            return Ok();
        }

    }
}
