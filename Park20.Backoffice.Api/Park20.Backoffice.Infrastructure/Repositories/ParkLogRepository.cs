using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Park20.Backoffice.Core.IRepositories;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class ParkLogRepository(IConfiguration configuration) : IParkLogRepository
    {
        public async Task<ParkLog?> CreateParkLog(ParkLog parkLog, DateTime startingDateTime)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(ParkLogQueries.CreateParkLog,
                new { parkLog.LicensePlate, parkLog.ParkName, StartDateTime = startingDateTime }
            );

            if (result > 0)
                return parkLog;

            return default;
        }

        public async Task<bool> UpdateParkLogWithEndingTime(ParkLog parkLog)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            var result = await connection.ExecuteAsync(
                ParkLogQueries.UpdateParkLogEndingTime,
                new { EndingTime = parkLog.EndTime, LicensePlate = parkLog.LicensePlate, ParkName = parkLog.ParkName }
            );

            if (result > 0)
                return true;

            return false;
        }


        public async Task<bool> UpdateAvailableParkingSpots(string parkName, string licensePlate, bool isEntrance)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var rowsUpdated = await dbConnection.ExecuteAsync(ParkQueries.UpdateAvailableParkingSpots, new { ParkName = parkName, LicensePlate = licensePlate, Entrance = isEntrance });

                return rowsUpdated > 0 ? true : false;
            }
        }

        public async Task<ParkLog> GetParkLog(string licensePlate)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var parkLog = await dbConnection.QueryFirstAsync<ParkLog>(ParkLogQueries.GetParkLog, new {LicensePlate=licensePlate});

                return parkLog;
            }
        }
    }
}
