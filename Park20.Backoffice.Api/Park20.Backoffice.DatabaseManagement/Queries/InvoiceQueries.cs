using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class InvoiceQueries
    {
        public static string CreateInvoice =>
            @"INSERT INTO Invoice (ParkLogId, IsPayed, TotalAmount) 
              VALUES (
                 (SELECT TOP (1) pl.Id FROM ParkLog pl 
                    INNER JOIN Vehicle v ON pl.VehicleId = v.Id 
                    WHERE v.LicensePlate = @LicensePlate 
                    ORDER BY pl.EndTime DESC
                 ),
                 @IsPayed,
                 @TotalCost
             )";
    }
}
