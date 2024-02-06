using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Park20.Backoffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {

        private readonly IUserService _userService = userService;

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerRequestDto createCustomerRequestDto)
        {
            if (await _userService.CheckIfEmailExists(createCustomerRequestDto.Email))
            {
                return BadRequest(new { Title = "Email already in use", Detail = "Please use a different email address." });
            }
            var customer = await _userService.AddCustomer(createCustomerRequestDto);
            return Ok(customer);
        }

        [HttpGet]
        public async Task<IActionResult> CheckIfUserIsRegistered([FromQuery] string username, string password)
        {
            var response = await _userService.CheckIfUserIsRegistered(username, password);
            return Ok(response);
        }

        [HttpGet("GetUser/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            var user = await _userService.GetUserByUsername(username);
            return Ok(user);
        }
        
        [HttpGet("GetUsersBeforeDate")]
        public async Task<IActionResult> GetUsersBeforeDate([FromQuery] DateTime time)
        {
            var user = await _userService.GetUsersBeforeDate(time);
            return Ok(user);
        }
    }
}
