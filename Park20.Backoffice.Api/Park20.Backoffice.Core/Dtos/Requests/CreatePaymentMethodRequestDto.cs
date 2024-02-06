using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public record CreatePaymentMethodRequestDto(int LastFourDigits, DateTime ExpirationDate, string FullName, string Token, string Username );
}
