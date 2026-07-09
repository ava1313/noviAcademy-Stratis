using WorldRank.Domain;

namespace WorldRank.Application.Strategies;

internal interface IFundsStrategy
{
    FundsOperation Operation { get; }

    void Execute(Wallet wallet, decimal amount);
}