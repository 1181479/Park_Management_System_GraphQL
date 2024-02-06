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
using Park20.Backoffice.Core.Domain.Park;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class ParkyCoinsConfigurationRepository(IConfiguration configuration) : IParkyCoinsConfigurationRepository
    {
        public async Task<int> GetBulkValue()
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                return await dbConnection.QuerySingleAsync<int>(ParkyCoinsConfigurationQueries.GetBulkValue);
            }
        }

        public async Task<int> GetCurrencyValue()
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                return await dbConnection.QuerySingleAsync<int>(ParkyCoinsConfigurationQueries.GetCurrencyValue);
            }
        }

        public async Task<int> GetNewCustomerValue()
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                return await dbConnection.QuerySingleAsync<int>(ParkyCoinsConfigurationQueries.GetRegestryValue);
            }
        }

        public async Task<int> GetParkingValue()
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                return await dbConnection.QuerySingleAsync<int>(ParkyCoinsConfigurationQueries.GetParkingValue);
            }
        }

        public async Task<bool> UpdateBulkValue(int value)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                await dbConnection.ExecuteAsync(ParkyCoinsConfigurationQueries.UpdateBulkValue, new { Value = value });
                return true;
            }
        }

        public async Task<bool> UpdateCurrencyValue(int value)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                var result = await dbConnection.ExecuteAsync(ParkyCoinsConfigurationQueries.UpdateCurrencyValue, new { Value = value });
                if(result > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> UpdateNewCustomerValue(int value)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                await dbConnection.ExecuteAsync(ParkyCoinsConfigurationQueries.UpdateRegestryValue, new { Value = value });
                return true;
            }
        }

        public async Task<bool> UpdateParkingValue(int value)
        {
            using IDbConnection dbConnection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            {
                await dbConnection.ExecuteAsync(ParkyCoinsConfigurationQueries.UpdateParkingValue, new { Value = value });
                return true;
            }
        }
    }
}
