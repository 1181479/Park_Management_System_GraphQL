using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Park20.Backoffice.Core.Domain;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class DashboardRepository(IConfiguration configuration) : IDashboardRepository
    {

        public async Task<List<DashboardElements>> GetUsersWithMostParkyCoinsUsage(string? parkname, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes)
        {
            var query = DashboardQueries.GetTotalParkyCoinsQuery(parkname, initialDate, endDate, vehicleType);

            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            await connection.QueryAsync(query);
            var result = await connection.QueryAsync<DashboardElements>(DashboardQueries.QueryParametersBestUsers(20));

            connection.Close();

            return result.ToList();

        }

        public async Task<List<DashboardElements>> GetUsersWithLessParkyCoinsUsage(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes)
        {
            var query = DashboardQueries.GetTotalParkyCoinsQuery(parkName, initialDate, endDate, vehicleType);

            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            await connection.QueryAsync(query);
            var result = await connection.QueryAsync<DashboardElements>(DashboardQueries.QueryParametersWorstUsers(20));

            connection.Close();

            return result.ToList();
        }

        public async Task<List<DashboardElements>> GetUsersWithMidParkyCoinsUsage(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType, int? totalMinutes)
        {
            var query = DashboardQueries.GetTotalParkyCoinsQuery(parkName, initialDate, endDate, vehicleType);

            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            await connection.QueryAsync(query);
            var result = await connection.QueryAsync<DashboardElements>(DashboardQueries.updateQueryParametersMidUsers(20));

            connection.Close();

            return result.ToList();
        }
    }
}
