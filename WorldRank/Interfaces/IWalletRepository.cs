using WorldRank.Classes;

namespace WorldRank.Interfaces;

public interface IWalletRepository
{
    void Add(Wallet wallet, int playerId);
    List<Wallet> GetByPlayer(int playerId);
}