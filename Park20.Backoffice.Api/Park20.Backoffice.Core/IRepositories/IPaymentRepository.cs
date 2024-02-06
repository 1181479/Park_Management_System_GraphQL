using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Domain.Payment;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IPaymentRepository
    {
        Task<string?> GetTokenFromLicensePlate(string licensePlate);
        Task<PaymentMethod?> AddPaymentMethod(PaymentMethod payment, string username);
        Task<IEnumerable<PaymentMethod>> GetAllFromUser(string username);
      
    }

}
