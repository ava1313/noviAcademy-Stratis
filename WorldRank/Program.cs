
List<Player> players = new List<Player>();


int nextId = 1;
bool running = true;



    while (running)
    {
        Console.WriteLine("1. Add Player");
        Console.WriteLine("2. Display Players");
        Console.WriteLine("3. Find player by name");
        Console.WriteLine("4. Exit");
        Console.Write("Select an option: ");
        



    string inputText = Console.ReadLine();
    Console.WriteLine("");
    
    int input = int.Parse(inputText);
   
        if (input == 1)
        {
            Console.Write("Player name: ");
            string name = Console.ReadLine();

            Console.Write("Player score: ");
            string scoreText = Console.ReadLine();
            int score = int.Parse(scoreText);

            Player player = new Player(nextId, name, score);
            players.Add(player);

            nextId++;
        Console.WriteLine("===========================================");
        Console.WriteLine("        Player added successfully.");
        Console.WriteLine("===========================================");

        }
        else if (input == 2)
        {
            Console.WriteLine("Players:");
            foreach (var player in players)
            {
                Console.WriteLine("Id: " + player.Id + ", Name: " + player.Name + ", Score: " + player.Score);
            }
        }
            else if (input == 3)
        {
            Console.Write("Enter player name to search: ");
            string searchName = Console.ReadLine();
            var foundPlayer = players.FirstOrDefault(p => p.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));
            if (foundPlayer != null)
            {
            Console.WriteLine("===========================================");
            Console.WriteLine("Found Player - Id: " + foundPlayer.Id + ", Name: " + foundPlayer.Name + ", Score: " + foundPlayer.Score);
            Console.WriteLine("===========================================");

        }
            else
            {
             Console.WriteLine("===========================================");
             Console.WriteLine("             Player not found.");
             Console.WriteLine("===========================================");

        }
    }
        else if (input == 4)
        {
            running = false;
        }
        else
        {
            Console.WriteLine("Invalid option. Please try again.");
        }
    }
