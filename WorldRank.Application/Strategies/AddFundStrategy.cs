using WorldRank.Domain;

namespace WorldRank.Application.Strategies;

internal class AddFundStrategy : IFundsStrategy
{
    public FundsOperation Operation => FundsOperation.Add;

    public void Execute(Wallet wallet, decimal amount)
    {
        wallet.Deposit(amount);
    }
}