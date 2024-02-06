namespace PaymentSimulation.Models
{
    public record GenerateTokenRequest(long CardNumber, int Cvv, string FullName, DateTime ExpirationDate);
}
