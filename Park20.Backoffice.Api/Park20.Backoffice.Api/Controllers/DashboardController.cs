using Microsoft.AspNetCore.Mvc;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {

        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("DashBoardBestUsers")]
        public async Task<IActionResult> CreateDashBoardMostSpenders([FromQuery] CreateDashboardUsageParkyCoinsRequestDto createDashboardUsage)
        {
            var result = await _dashboardService.GetUsersWithMostParkyCoinsSpent(createDashboardUsage.parkName, createDashboardUsage.initialDate, createDashboardUsage.endDate, createDashboardUsage.vehicleType, createDashboardUsage.totalMinutes);

            return Ok(result);
        }

        [HttpGet("DashBoardWorstUsers")]
        public async Task<IActionResult> CreateDashBoardWorstSpenders([FromQuery] CreateDashboardUsageParkyCoinsRequestDto createDashboardUsage)
        {
            var result = await _dashboardService.GetUsersWithLessParkyCoinsSpent(createDashboardUsage.parkName, createDashboardUsage.initialDate, createDashboardUsage.endDate, createDashboardUsage.vehicleType, createDashboardUsage.totalMinutes);

            return Ok(result);
        }

        [HttpGet("DashBoardMidUsers")]
        public async Task<IActionResult> CreateDashBoardMidSpenders([FromQuery] CreateDashboardUsageParkyCoinsRequestDto createDashboardUsage)
        {
            var result = await _dashboardService.GetUsersWithMidParkyCoinsSpent(createDashboardUsage.parkName, createDashboardUsage.initialDate, createDashboardUsage.endDate, createDashboardUsage.vehicleType, createDashboardUsage.totalMinutes);

            return Ok(result);
        }

    }
}
