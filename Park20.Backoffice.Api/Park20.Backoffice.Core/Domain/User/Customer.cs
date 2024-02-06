using Park20.Backoffice.Core.Domain.ParkyWallets;

namespace Park20.Backoffice.Core.Domain.User;
public class Customer : User
{
    public bool Blocked { get; set; }
    public string? Invitecode { get; set; }
    public Vehicle[] Vehicles { get; set; }

    public PaymentMethod[] PaymentMethods { get; set; }
    public int ParkyWalletId { get; set; }

    public Customer() : base()
    { }

    public Customer(string username, string email, string password, string name, bool blocked, string? invitecode)
    : base(name, email, password, username)
    {
        Blocked = blocked;
        Invitecode = invitecode;
    }
}

