using Park20.Backoffice.Core.Domain;
using Park20.Backoffice.Core.Domain.ParkyWallets;
using Park20.Backoffice.Core.Domain.User;
using Park20.Backoffice.Core.Dtos.Requests;
using Park20.Backoffice.Core.Dtos.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.Application.Mappers
{
    public static class ParkyWalletMapper
    {
        public static ParkyWalletDto ToParkyWalletDto(ParkyWallet parkyWallet)
        {

            return new ParkyWalletDto() { CurrentBalance = parkyWallet.CurrentBalance, Movements = parkyWallet.Movements.ToParkyWalletMovementDto().ToList() };
        }

        public static IEnumerable<ParkyWalletMovementDto> ToParkyWalletMovementDto(this List<ParkyWalletMovements> movements)
        {
            foreach (var item in movements)
            {
                yield return new ParkyWalletMovementDto() { Amount = item.Amount, Date = item.Date, MovementType = item.Type.ToString()};
            }
        }
    }
}
