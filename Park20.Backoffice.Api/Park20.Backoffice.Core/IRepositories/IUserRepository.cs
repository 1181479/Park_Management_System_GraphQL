using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> GetCustomer(string email);
        Task<List<Customer>> AllCustomer();
        Task<bool> CheckIfUserIsRegistered(string username, string password);
        Task<Customer> GetUserByUsername(string username);
        Task<bool> CheckIfEmailExists(string email);
        Task<Customer> GetCustomerByLicensePlate(string licensePlate);
    }
}
