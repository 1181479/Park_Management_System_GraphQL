using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;

namespace Park20.Backoffice.Application.Mappers
{
    public static class UserMapper
    {

        public static List<CreateCustomerResultDto> ToCustomerDto(List<Customer> customer)
        {
            List<CreateCustomerResultDto> result = [];
            foreach (var customerDto in customer)
            {
                result.Add(ToCustomerDto(customerDto));
            }
            return result;
        }
        public static CreateCustomerResultDto ToCustomerDto(Customer customer)
        {
            return new CreateCustomerResultDto(customer.Name, customer.Email, customer.Username);
        }
        public static GetCustomerResultDto ToGetCustomerDto(Customer customer)
        {
            return new GetCustomerResultDto(customer.Name, customer.Email, customer.Username);
        }

        public static Customer ToCustomerDomain(CreateCustomerRequestDto userDto)
        {
            return new Customer(userDto.Username, userDto.Email, userDto.Password ,userDto.Name, false, default);
        }

    }
}
