using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;

namespace Park20.Backoffice.Core.IServices
{
    public interface IUserService
    {
        Task<CreateCustomerResultDto> AddCustomer(CreateCustomerRequestDto createCustomerRequestDto);
        Task<bool> CheckIfUserIsRegistered(string username, string password);
        Task<CreateCustomerResultDto>GetUserByUsername(string username);
        Task<List<CreateCustomerResultDto>>GetUsersBeforeDate(DateTime time);
        Task<bool> CheckIfEmailExists(string email);
    }
}
