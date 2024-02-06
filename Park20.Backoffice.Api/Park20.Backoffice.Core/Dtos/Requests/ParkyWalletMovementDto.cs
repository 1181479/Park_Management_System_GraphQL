using Park20.Backoffice.Core.Domain.ParkyWallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Dtos.Requests
{
    public class ParkyWalletMovementDto
    {
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string MovementType { get; set; }
    }
}
