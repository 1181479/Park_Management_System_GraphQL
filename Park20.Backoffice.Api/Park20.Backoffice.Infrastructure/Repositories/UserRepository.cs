using Microsoft.Extensions.Configuration;
using Park20.Backoffice.Core.IRepositories;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Park20.Backoffice.DatabaseManagement.Sql.Queries;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Domain.Park;
using BCrypt.Net;

namespace Park20.Backoffice.Infrastructure.Repositories
{
    public class UserRepository(IConfiguration configuration) : IUserRepository
    {
        
        public async Task<Customer?> AddCustomer(Customer customer)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            var result = await connection.ExecuteAsync(CustomerQueries.AddCustomer, new { customer.Name, customer.Email, hashedPassword, customer.Username, customer.ParkyWalletId });
            if(result > 0)
            {
                return customer;
            }
            return default;
            
        }

        public async Task<Customer> GetCustomer(string email)
        {
            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<Customer>(CustomerQueries.CustomerByEmail, new { CustomerEmail = email });
        }

        public async Task<List<Customer>> AllCustomer()
        {
            using SqlConnection connection = new(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            var customers = await connection.QueryAsync<Customer>(CustomerQueries.AllCustomer);

            // Convert the IEnumerable to a List
            return customers.ToList();
        }

        public async Task<bool> CheckIfUserIsRegistered(string username, string password)
        {
            using SqlConnection connection = new(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            var hashedPasswordFromDatabase = await connection.QuerySingleOrDefaultAsync<string>(CustomerQueries.GetHashedPassword, new { Username = username });

            
            if(hashedPasswordFromDatabase != null && BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDatabase))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            using SqlConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnection"));
            connection.Open();

            var existingEmail = await connection.QuerySingleOrDefaultAsync<string>(CustomerQueries.GetCustomerByEmail, new { Email = email });

            return existingEmail != null;
        }
        public async Task<Customer> GetUserByUsername(string username)
        {
            using SqlConnection connection = new(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            return await connection.QuerySingleAsync<Customer>(CustomerQueries.GetUserByUsername, new { Username = username });
        }

        public async Task<Customer> GetCustomerByLicensePlate(string licensePlate)
        {
            using SqlConnection connection = new(configuration.GetConnectionString("DBConnection"));
            connection.Open();
            return await connection.QuerySingleAsync<Customer>(CustomerQueries.GetUserByLicensePlate, new { LicensePlate = licensePlate });
        }
    }
}
