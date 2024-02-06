using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IDashboardRepository
    {
        Task<List<DashboardElements>> GetUsersWithLessParkyCoinsUsage(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
        Task<List<DashboardElements>> GetUsersWithMidParkyCoinsUsage(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
        Task<List<DashboardElements>> GetUsersWithMostParkyCoinsUsage(string? parkname, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes);
    }
}
