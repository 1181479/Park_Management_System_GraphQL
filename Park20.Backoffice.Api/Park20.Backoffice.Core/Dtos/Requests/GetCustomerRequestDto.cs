namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class GetCustomerRequestDto
    {
        public string Email { get; set; }

        public GetCustomerRequestDto()
        {
            
        }

        public GetCustomerRequestDto(string email)
        {
            Email = email;
        }
    }
}
