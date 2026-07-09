namespace WorldRank.Exceptions;

public class InsufficientFundsException : WalletException
{
    public InsufficientFundsException(decimal balance, decimal amount)
        : base($"Insufficient funds. Balance: {balance}, Requested: {amount}") { }
}
