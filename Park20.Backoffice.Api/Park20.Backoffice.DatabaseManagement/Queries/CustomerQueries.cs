using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class CustomerQueries
    {
        public static string AllCustomer => "SELECT * FROM [Customer] (NOLOCK)";

        public static string CustomerById => "SELECT * FROM [Customer] (NOLOCK) WHERE [CustomerId] = @CustomerId";
        public static string CustomerByEmail => "SELECT * FROM [Customer] (NOLOCK) WHERE [Email] like @CustomerEmail";

        public static string AddCustomer =>
            @"INSERT INTO [Customer] ([Name], [Email], [Password], [Username], [Blocked], [ParkyWalletId], [RegistrationDate]) 
            VALUES (@Name, @Email, @hashedPassword, @Username, 0, @ParkyWalletId, GETDATE())";

        public static string CheckCustomer => @"SELECT *
            FROM [dbo].[Customer] WHERE Username = @Username and [Password]=@Password;";

        public static string GetHashedPassword => @"SELECT Password
            FROM [dbo].[Customer] WHERE Username = @Username";

        public static string GetUserByUsername => @"SELECT * FROM [dbo].[Customer] WHERE Username = @Username";
        public static string GetCustomerByEmail => @"SELECT * FROM [dbo].[Customer] WHERE Email = @Email";
        public static string GetUserByLicensePlate => @"SELECT c.* FROM [Customer] c INNER JOIN [Vehicle] v on c.Id = v.CustomerId WHERE v.LicensePlate = @LicensePlate";
    }
}
