using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class VehicleQueries
    {
        public static string AddVehicle => @"INSERT INTO [dbo].[Vehicle] ([LicensePlate], [Model], [Brand], [Type], [CustomerId])
                            SELECT @licensePlate, @model, @brand, @type, [Id]
                            FROM [dbo].[Customer]
                            WHERE [Username] = @username;";

        public static string VehicleByLicencePlate => "SELECT * FROM [Vehicle] (NOLOCK) WHERE [LicensePlate] like @LicencePlate";
        public static string GetAllFromUser => @"SELECT * FROM Vehicle
                              INNER JOIN Customer on Customer.Id = Vehicle.CustomerId
                              WHERE Customer.Username = @username";
    }
}
