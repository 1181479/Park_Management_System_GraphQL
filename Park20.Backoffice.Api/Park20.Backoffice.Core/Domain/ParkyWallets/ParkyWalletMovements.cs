namespace Park20.Backoffice.Core.Domain.ParkyWallets
{
    public class ParkyWalletMovements
    {
        public int MovementId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public MovementType Type { get; set; }
    }
}
