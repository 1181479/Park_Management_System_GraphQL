using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.LicensePlateReaderExit.Core.Models.Dtos
{
    public class HybridPaymentDto(decimal parkyCoinsAmount, decimal otherPaymentMethodAmount, decimal totalCost, bool isSuccessfull)
    {
        public decimal parkyCoinsAmount { get; private set; } = parkyCoinsAmount;
        public decimal otherPaymentMethodAmount { get; private set; } = otherPaymentMethodAmount;
        public decimal totalCost { get; private set; }  = totalCost;
        public bool isSuccessfull { get; private set; } = isSuccessfull;
    }
}
