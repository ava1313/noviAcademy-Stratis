public class Player
{
    public int Id { get; private set; }
    public string Name { get; set; }

    public int Score { get; set; }

    public Player(int id, string name, int score)
    {
        Id=id;
        Name = name;
        Score = score;
    }
}