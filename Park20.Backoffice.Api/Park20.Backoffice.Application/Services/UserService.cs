using Park20.Backoffice.Application.Mappers;
using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using Park20.Backoffice.Core.IRepositories;
using Park20.Backoffice.Core.IServices;

namespace Park20.Backoffice.Application.Services
{
    public class UserService(IUserRepository userRepository, IParkyWalletService parkyWalletService) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IParkyWalletService parkyWalletService = parkyWalletService;

        public async Task<CreateCustomerResultDto> AddCustomer(CreateCustomerRequestDto createCustomerRequestDto)
        {
            Customer customer = UserMapper.ToCustomerDomain(createCustomerRequestDto);
            customer.ParkyWalletId = (await parkyWalletService.Create()).Id;
            return UserMapper.ToCustomerDto(await userRepository.AddCustomer(customer));
        }

        public async Task<bool> CheckIfUserIsRegistered(string username, string password)
        {
            return await userRepository.CheckIfUserIsRegistered(username, password);
        }

        public async Task<CreateCustomerResultDto> GetUserByUsername(string username)
        {
            return UserMapper.ToCustomerDto(await userRepository.GetUserByUsername(username));
        }

        public async Task<List<CreateCustomerResultDto>> GetUsersBeforeDate(DateTime time)
        {
            List<Customer> customers = await userRepository.AllCustomer();
            return UserMapper.ToCustomerDto(customers.FindAll(u => u.RegistrationDate < time));
        }

        public async Task<bool> CheckIfEmailExists(string email)
        {
            return await userRepository.CheckIfEmailExists(email);
        }
    }
}
