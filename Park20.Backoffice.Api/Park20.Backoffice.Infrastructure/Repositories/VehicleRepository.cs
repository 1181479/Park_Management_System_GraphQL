using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Park20.Backoffice.Core.Domain;
using Dapper;
using System.ComponentModel;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class VehicleRepository(IConfiguration configuration) : IVehicleRepository
    {
        public async Task<Vehicle?> AddVehicle(Vehicle vehicle, string username)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            var result = await connection.ExecuteAsync(VehicleQueries.AddVehicle, new { licensePlate = vehicle.LicensePlate, model = vehicle.Model, brand = vehicle.Brand, type = vehicle.Type, username = username });
            if (result > 0)
            {
                return vehicle;
            }
            return default;

        }

        public async Task<Vehicle?> GetVehicle(string licence)
        {
            using SqlConnection connection = new(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Vehicle>(VehicleQueries.VehicleByLicencePlate, new { LicencePlate = licence });
        }
        
        public async Task<IEnumerable<Vehicle>> GetAllFromUser(string username)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            return await connection.QueryAsync<Vehicle>(VehicleQueries.GetAllFromUser, new { username = username });
        }
    }
}
