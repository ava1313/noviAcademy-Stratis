namespace WorldRank;

public interface IPlayerRepository
{
    void AddPlayer(Player player);

    Player? FindPlayer(int playerId);

    bool DeletePlayer(int playerId);

    IEnumerable<IGrouping<int, Player>> GroupPlayersByScore();
}