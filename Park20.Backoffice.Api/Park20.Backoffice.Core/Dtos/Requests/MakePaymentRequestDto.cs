using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class MakePaymentRequestDto
    {
        public string Token {  get; set; }
        public decimal Amount { get; set; }
    }
}
