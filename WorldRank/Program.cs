using WorldRank.Classes;
using WorldRank.Enum;
using WorldRank.Interfaces;

IPlayerRepository playerRepository = new InMemoryPlayerRepository();
IWalletRepository walletRepository = new InMemoryWalletRepository();
ILogger logger = new ConsoleLogger();
logger.LogInfo("Application started.");
while (true)
{
    
    Console.WriteLine("\n=== WorldRank Player Registry ===");
    Console.WriteLine("1. Add player");
    Console.WriteLine("2. List players by score");
    Console.WriteLine("3. Find player by ID");
    Console.WriteLine("4. Delete player");
    Console.WriteLine("5. Add wallet to player");
    Console.WriteLine("6. List player wallets");
    Console.WriteLine("7. Deposit to wallet");
    Console.WriteLine("8. Withdraw from wallet");
    Console.WriteLine("0. Exit");
    Console.Write("> ");

    string? choice = Console.ReadLine();

    if (choice == "1")
        AddPlayer();
    else if (choice == "2")
        ListPlayersByScore();
    else if (choice == "3")
        FindPlayer();
    else if (choice == "4")
        DeletePlayer();
    else if (choice == "5")
        AddWallet();
    else if (choice == "6")
        ListWallets();
    else if (choice == "7")
        DepositToWallet();
    else if (choice == "8")
        WithdrawFromWallet();
    else if (choice == "0")
        break;
    else
        Console.WriteLine("Invalid option.");
}

void AddPlayer()
{
    Console.Write("Name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        logger.LogWarning("User entered empty player name.");
        Console.WriteLine("Name cannot be empty.");
        return;
    }

    Console.Write("Score: ");
    if (!int.TryParse(Console.ReadLine(), out int score))
    {
        logger.LogWarning("Invalid score entered.");
        Console.WriteLine("Score must be a number.");
        return;
    }

    Player player = new Player(name);
    player.UpdateScore(score);

    playerRepository.AddPlayer(player);
    Console.WriteLine($"Player added. ID: {player.Id}");
}

void ListPlayersByScore()
{
    var groups = playerRepository.GroupPlayersByScore();

    foreach (var group in groups)
    {
        Console.WriteLine($"\nScore: {group.Key}");

        foreach (var player in group)
        {
            Console.WriteLine(player);
        }
    }
}

void FindPlayer()
{
    Console.Write("Player ID: ");

    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int playerId))
    {
        logger.LogWarning($"Invalid player id input: {input}");
        Console.WriteLine("Invalid ID.");
        return;
    }

    Player? player = playerRepository.FindPlayer(playerId);

    if (player == null)
    {
        Console.WriteLine("Player not found.");
        return;
    }
    
    Console.WriteLine(player);

}

void DeletePlayer()
{
    var groupedPlayers = playerRepository.GroupPlayersByScore();

    var players = groupedPlayers
        .SelectMany(group => group)
        .OrderBy(p => p.Id)
        .ToList();

    if (!players.Any())
    {
        Console.WriteLine("There are no players to delete.");
        return;
    }

    Console.WriteLine("=== Players ===");

    foreach (var player in players)
    {
        Console.WriteLine(player);
    }

    Console.Write("Enter Player ID to delete: ");
    string? input = Console.ReadLine();

    if (!int.TryParse(input, out int playerId) || playerId <= 0)
    {
        Console.WriteLine("Invalid ID.");
        return;
    }

    Player? playerToDelete = playerRepository.FindPlayer(playerId);

    if (playerToDelete == null)
    {
        logger.LogWarning($"Delete failed. Player {playerId} not found.");
        Console.WriteLine("Player not found.");
        return;
    }


    bool deleted = playerRepository.DeletePlayer(playerId);

    if (deleted)
    {
        logger.LogInfo($"Player {playerId} deleted.");
        Console.WriteLine("Player deleted.");
    }
}
void AddWallet()
{
    Console.Write("Player ID: ");

    if (!int.TryParse(Console.ReadLine(), out int playerId))
    {
        Console.WriteLine("Invalid ID.");
        return;
    }

    Player? player = playerRepository.FindPlayer(playerId);

    if (player == null)
    {
        Console.WriteLine("Player not found.");
        return;
    }

    Console.WriteLine("Available currencies:");

    foreach (var item in Enum.GetValues<Currency>())
    {
        Console.WriteLine(item);
    }

    Console.Write("Currency: ");
    string? currencyInput = Console.ReadLine();

    if (!Enum.TryParse(currencyInput, true, out Currency selectedCurrency))
    {
        Console.WriteLine("Invalid currency.");
        return;
    }

    Wallet wallet = new Wallet(player, selectedCurrency);

    try
    {
        walletRepository.Add(wallet, playerId);
        logger.LogInfo($"Wallet {selectedCurrency} created for player {playerId}");
        Console.WriteLine("Wallet added.");
    }
    catch (Exception ex)
    {
        logger.LogError("Wallet creation failed.", ex);
        Console.WriteLine(ex.Message);

    }
}

void ListWallets()
{
    Console.Write("Player ID: ");

    if (!int.TryParse(Console.ReadLine(), out int playerId))
    {
        Console.WriteLine("Invalid ID.");
        return;
    }

    var wallets = walletRepository.GetByPlayer(playerId);

    if (wallets.Count == 0)
    {
        Console.WriteLine("No wallets found.");
        return;
    }

    foreach (var wallet in wallets)
    {
        Console.WriteLine(wallet);
    }
}

void DepositToWallet()
{
    Wallet? wallet = SelectWallet();

    if (wallet == null)
        return;

    Console.Write("Amount: ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    try
    {
        wallet.Deposit(amount);
        logger.LogInfo($"Deposit of {amount} to wallet {wallet.Id}");
        Console.WriteLine("Deposit completed.");
    }
    catch (Exception ex)
    {
        logger.LogError("Deposit failed.", ex);
        Console.WriteLine(ex.Message);
    }
}

void WithdrawFromWallet()
{
    Wallet? wallet = SelectWallet();

    if (wallet == null)
        return;

    Console.Write("Amount: ");

    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    try
    {

        wallet.Withdraw(amount);
        logger.LogInfo($"Withdraw {amount} from wallet {wallet.Id}");
        Console.WriteLine("Withdraw completed.");
    }
    catch (Exception ex)
    {
        logger.LogError("Withdraw failed.", ex);
        Console.WriteLine(ex.Message);
    }
}

Wallet? SelectWallet()
{
    Console.Write("Player ID: ");

    if (!int.TryParse(Console.ReadLine(), out int playerId))
    {
        Console.WriteLine("Invalid ID.");
        return null;
    }

    var wallets = walletRepository.GetByPlayer(playerId);

    if (wallets.Count == 0)
    {
        Console.WriteLine("No wallets found.");
        return null;
    }

    foreach (var wallet in wallets)
    {
        Console.WriteLine($"Wallet ID: {wallet.Id} | {wallet.Currency} | Balance: {wallet.Balance}");
    }

    Console.Write("Wallet ID: ");

    if (!int.TryParse(Console.ReadLine(), out int walletId))
    {
        Console.WriteLine("Invalid wallet ID.");
        return null;
    }

    Wallet? selectedWallet = wallets.FirstOrDefault(w => w.Id == walletId);

    if (selectedWallet == null)
    {
        Console.WriteLine("Wallet not found.");
        return null;
    }

    return selectedWallet;
}