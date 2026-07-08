namespace WorldRank;

public class Wallet
{
    private static int _nextId = 1;
    public int Id { get; }
    public IPlayer Player { get; }
    public decimal Balance { get; private set; }
    public Currency Currency { get; }
    public bool IsBlocked { get; private set; }
    public Wallet(IPlayer player, Currency currency)
    {
        Id = _nextId++;
        Player = player ?? throw new ArgumentNullException(nameof(player));
        Currency = currency;
        Balance = 0m;
        IsBlocked= false;   
    }
    public void Deposit(decimal amount)
    {
        if (IsBlocked)
            throw new InvalidOperationException("Wallet is blocked.");

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Deposit amount must be positive.");

        Balance += amount;
    }
    public void Withdraw(decimal amount)
    {
        if (IsBlocked)
            throw new InvalidOperationException("Wallet is blocked.");

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Withdrawal amount must be positive.");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount;
    }
    public void Block()
    {
        IsBlocked = true;
    }

    public override string ToString()
    {
        return $"Wallet ID: {Id}, Player: {Player.Name}, Balance: {Balance} {Currency}, Blocked: {IsBlocked}";
    }
}
