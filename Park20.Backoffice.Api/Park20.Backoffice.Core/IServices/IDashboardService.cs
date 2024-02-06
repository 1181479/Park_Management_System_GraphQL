using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IServices
{
    public interface IDashboardService
    {
        Task<List<DashboardElements>> GetUsersWithLessParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
        Task<List<DashboardElements>> GetUsersWithMidParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
        Task<List<DashboardElements>> GetUsersWithMostParkyCoinsSpent(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
    }
}
