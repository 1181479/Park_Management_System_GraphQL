using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain.User
{
    public class PaymentMethod
    {
        public string FullName { get; set; }
        public int CardLastFourDigits { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string PaymentToken { get; set; }
    }
}
