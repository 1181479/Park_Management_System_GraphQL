using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park20.Backoffice.Core.Domain.ParkyWallets;

namespace Park20.Backoffice.Core.IRepositories
{
    public interface IParkyWalletRepository
    {
        Task<ParkyWallet> Create(int regestryValue);
        Task<bool> BulkAdd(List<int> customerIds, int bulkValue);
        Task<ParkyWallet> GetParkyWalletByUsername(string username);
        Task UpdateCurrentBalance(int customerParkyWalletId, decimal amount);
    }
}
