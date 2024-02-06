using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateInvoice(Invoice invoice, string licensePlate);
    }
}
