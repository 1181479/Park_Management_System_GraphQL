using Dapper;
using System.Diagnostics.Metrics;
using System.Text;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class DashboardQueries
    {

        public static string QueryParametersBestUsers(int percentage) =>
            $"SELECT TOP {percentage} PERCENT Username, SUM(TotalParkyCoinsSpent) as ParkyCoinsSpent FROM DashBoardView GROUP BY Username ORDER BY SUM(TotalParkyCoinsSpent) DESC";

        public static string QueryParametersWorstUsers(int percentage) =>
            $"SELECT TOP {percentage} PERCENT Username, SUM(TotalParkyCoinsSpent) as ParkyCoinsSpent FROM DashBoardView GROUP BY Username ORDER BY SUM(TotalParkyCoinsSpent) ASC";


        public static string updateQueryParametersMidUsers(int percentage)
        {
            var x = $"SELECT Username, SUM(TotalParkyCoinsSpent) as ParkyCoinsSpent FROM DashBoardView " +
            $"WHERE Username NOT IN (" +
                       $"SELECT TOP 20 PERCENT Username FROM DashBoardView GROUP BY Username ORDER BY SUM(TotalParkyCoinsSpent) DESC" +
            $")" +
            $"AND Username NOT IN (" +
                        $"SELECT TOP 20 PERCENT Username FROM DashBoardView GROUP BY Username ORDER BY SUM(TotalParkyCoinsSpent) ASC" +
            $")" +
            $"GROUP BY Username ORDER BY SUM(TotalParkyCoinsSpent) ASC";

            return x;
        }
           



        public static string GetTotalParkyCoinsQuery(string? parkName, DateTime? initialDate, DateTime? endDate, string? vehicleType)
        {
            var query = new StringBuilder();
            //var parameters = new DynamicParameters();


            query.AppendLine("CREATE OR ALTER VIEW DashBoardView AS (");
            query.AppendLine("SELECT c.Username, SUM(pwm.Amount) AS TotalParkyCoinsSpent FROM Customer c");
            query.AppendLine("INNER JOIN Vehicle v ON v.CustomerId = c.Id");
            query.AppendLine("INNER JOIN ParkLog pl ON v.Id = pl.VehicleId");
            query.AppendLine("INNER JOIN Park p ON p.Id = pl.ParkId");
            query.AppendLine("INNER JOIN ParkyWallet pw ON c.ParkyWalletId = pw.Id");
            query.AppendLine("INNER JOIN ParkyWalletMovements pwm ON pwm.ParkyWalletId = pw.Id");
            query.AppendLine("WHERE Lower(pwm.MovementType) = 'outbound' AND ABS(DATEDIFF(MINUTE, pl.EndTime, pwm.Date)) <= 2 ");

            if (!string.IsNullOrWhiteSpace(parkName))
            {
                query.AppendLine($"AND p.ParkName = '{parkName}'");
                //parameters.Add("@ParkName", parkName);
            }

            if (initialDate != null)
            {
                query.AppendLine($"AND pl.StartTime >= '{initialDate}' ");
                //parameters.Add("@InitialDate", initialDate);
            }

            if (endDate != null)
            {
                query.AppendLine($"AND pl.EndTime <= '{endDate}' ");
                //parameters.Add("@EndDate", endDate);
            }

            if (!string.IsNullOrWhiteSpace(vehicleType))
            {
                query.AppendLine($"AND v.Type = '{vehicleType}'");
                //parameters.Add("@VehicleType", vehicleType);
            }


            query.AppendLine("GROUP BY c.Username);");

            var x = query.ToString();

            return query.ToString();
        }
    }
}
