using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class ParkLogQueries
    {
        public static string CreateParkLog =>
                "INSERT INTO [ParkLog] (VehicleId, ParkId, StartTime) " +
                "SELECT " +
                    "(SELECT Id FROM [Vehicle] WHERE LicensePlate = @licensePlate) AS VehicleId, " +
                    "(SELECT Id FROM [Park] WHERE ParkName = @parkName) AS ParkId, " +
                    "@startDateTime AS StartTime";


        public static string UpdateParkLogEndingTime =>
            "UPDATE ParkLog " +
             "SET EndTime = @EndingTime " +
             "WHERE VehicleId = (SELECT Id FROM Vehicle WHERE LicensePlate = @LicensePlate) " +
             "AND ParkId = (SELECT Id FROM Park WHERE ParkName = @ParkName) " +
             "AND EndTime IS NULL";

        public static string GetParkLog =>
            @"SELECT TOP (1) * FROM ParkLog pl 
                    INNER JOIN Vehicle v ON pl.VehicleId = v.Id 
                    WHERE v.LicensePlate = @LicensePlate 
                    ORDER BY pl.EndTime DESC";
    }
}
