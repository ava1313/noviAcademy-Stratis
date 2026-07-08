using WorldRank.Interfaces;

namespace WorldRank.Classes;

public class InMemoryPlayerRepository : IPlayerRepository
{
    private readonly List<Player> _players = new();

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public Player? FindPlayer(int playerId)
    {
        return _players.FirstOrDefault(p => p.Id == playerId);
    }

    public bool DeletePlayer(int playerId)
    {
        var player = FindPlayer(playerId);

        if (player == null)
            return false;

        _players.Remove(player);
        return true;
    }

    public IEnumerable<IGrouping<int, Player>> GroupPlayersByScore()
    {
        return _players.GroupBy(p => p.Score);
    }
}