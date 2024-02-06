using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class ParkyWalletQueries
    {
        public static string BulkAssign => @"UPDATE [ParkyWallet] SET Amount = Amount + @Amount WHERE Id IN @ParkyWalletIds";
        public static string CreateWallet => @"INSERT INTO [ParkyWallet] ([Amount]) VALUES (@Amount)";
        public static string CreateMovement => @"INSERT INTO [ParkyWalletMovements] ([Amount], [Date], [MovementType], [ParkyWalletId]) VALUES (@Amount, @Date, @MovementType, @ParkyWalletId)";

        public static string GetParkyWalletByUserName => @"SELECT pw.Id AS Id, pw.Amount AS CurrentBalance, pwm.Id AS MovementId, 
                                                            pwm.Amount Amount, pwm.Date AS Date, pwm.MovementType AS Type
                                                            FROM ParkyWallet pw
                                                            INNER JOIN Customer c ON pw.Id = c.ParkyWalletId
                                                            INNER JOIN ParkyWalletMovements pwm ON pwm.ParkyWalletId = pw.Id
                                                            WHERE c.Username  = @Username";

    }
}
