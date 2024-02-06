using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.Domain.Park
{
    public class PriceTable
    {
        public int PriceTableId { get; set; }
        public DateTime InitialDate { get; set; }
        public List<LinePriceTable> LinePrices { get; set; }
    }
}
