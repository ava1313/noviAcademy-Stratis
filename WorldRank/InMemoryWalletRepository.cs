namespace WorldRank;


public class InMemoryWalletRepository : IWalletRepository
{
    private readonly Dictionary<int, Wallet> _wallets = new();

    public void Add(Wallet wallet, int playerId)
    {
        if (wallet == null)
            throw new ArgumentNullException(nameof(wallet));

        if (_wallets.ContainsKey(wallet.Id))
            throw new InvalidOperationException("Wallet with the same ID already exists.");

        _wallets[wallet.Id] = wallet;
    }

    public List<Wallet> GetByPlayer(int playerId)
    {
        return _wallets.Values
                       .Where(w => w.Player.Id == playerId)
                       .ToList();
    }
}