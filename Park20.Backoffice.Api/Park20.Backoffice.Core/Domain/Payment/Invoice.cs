using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain.Payment
{
    public class Invoice(decimal totalCost, bool isPayed)
    {
        public decimal TotalCost { get; set; } = totalCost;
        public bool IsPayed { get; set; } = isPayed;
    }
}
