namespace Park20.Backoffice.DatabaseManagement.Sql.Queries
{
    public class PaymentQueries
    {

        public static string PaymentTokenByLicensePlate =>
            "SELECT PaymentToken FROM [Vehicle] v INNER JOIN [PaymentMethod] pm ON v.CustomerId = pm.CustomerId WHERE v.LicensePlate = @licensePlate";
        public static string AddPaymentMethod => @"INSERT INTO [dbo].[PaymentMethod] ([CardLastFourDigits], [FullName], [ExpirationDate], [PaymentToken], [CustomerId])
                            SELECT @cardLastFourDigits, @fullName, @expirationDate, @paymentToken, [Id]
                            FROM [dbo].[Customer]
                            WHERE [Username] = @username;";
        public static string GetAllFromUser => @"SELECT * FROM PaymentMethod
                              INNER JOIN Customer on Customer.Id = PaymentMethod.CustomerId
                              WHERE Customer.Username = @username";
    }
}
