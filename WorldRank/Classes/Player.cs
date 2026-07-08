using WorldRank.Interfaces;

namespace WorldRank.Classes;

public class Player : IPlayer
{
	public static int _newId = 1;
	public int Id { get; }
	public string Name { get; }
	public int Score { get; private set; }

	public Player(string name)
	{
		if (string.IsNullOrEmpty(name))
			throw new ArgumentException("Name cannot be null or empty.", nameof(name));

		Id = _newId++;
        Name = name;
	}

	public void UpdateScore(int newScore)
	{
		if (newScore < 0)
			throw new ArgumentOutOfRangeException(nameof(newScore), "Score cannot be negative.");

		Score = newScore;
	}

	public override string ToString() =>
			$"[{Id}] {Name} - Score: {Score}";
}
