namespace Maze.Cmd
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Bienvenue dans Maze !");
            Console.ResetColor();
            Console.WriteLine("Entrer votre nom :");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
                name = "Jimmy";

            Player player = new Player(name);
            player.Name = name;
            player.LevelUp += player_LevelUp;
            player.PotionFound += player_PotionFound;
            player.ToolFound += player_ToolsFound;
            player.TrapAvoided += player_TrapAvoided;
            player.Trapped += player_Trapped;
            player.MonsterFound += player_MonsterFound;

            while (true)
            {
                Console.WriteLine("Voulez vous lancer le dé ? [Y/n]");

                var res = Console.ReadLine();
                if (res.ToLower() == "n")
                {
                    Console.WriteLine("Vous avez décidé de qutter la partie");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                int random = Dice.Roll(1, 7);
                int randomtool = Dice.Roll(1, 8);
                int randommonster = Dice.Roll(1, 7);
                switch (random)
                {
                    case 1:
                        Console.WriteLine($"Vous avez tiré le chiffre {random} !");
                        Trap trap = Trap.Generate();
                        player.Fall(trap);
                        if (player.HP <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Vous êtes mort !");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Fin de la partie !");
                            Console.ResetColor();
                            Environment.Exit(0);
                        }
                        break;

                    case 2:
                        Console.WriteLine($"Vous avez tiré le chiffre {random} !");
                        Tools? tools = Tools.Generate();
                        player.PickUp(tools);
                        break;

                    case 3:
                        Console.WriteLine($"Vous avez tiré le chiffre {random} !");
                        Potion potion = Potion.Generate();
                        player.PickUp(potion);
                        break;

                    case 4:
                        Console.WriteLine($"Vous avez tiré le chiffre {random} !");
                        Console.WriteLine("Vous allez à gauche !");
                        if (randomtool == 2)
                        {
                            Tools tool = Tools.Generate();
                            player.PickUp(tool);
                        }
                        if (randommonster == 6)
                        {
                            Monster monster = Monster.Generate();
                                player.Fight(monster);
                        }
                        player.Walk();
                        break;

                    case 5:
                        Console.WriteLine($"Vous avez tiré le chiffre {random} !");
                        Console.WriteLine("Vous allez en face !");
                        if (randomtool == 5)
                        {
                            Tools tool = Tools.Generate();
                            player.PickUp(tool);
                        }
                        if (randommonster == 6)
                        {
                            Monster monster = Monster.Generate();
                                player.Fight(monster);
                        }
                        player.Walk();
                        break;

                    case 6:
                        Console.WriteLine($"Vous avez tiré le chiffre {random} !");
                        Console.WriteLine("Vous allez à droite !");
                        if (randomtool == 7)
                        {
                            Tools tool = Tools.Generate();
                            player.PickUp(tool);
                        }
                        if (randommonster == 6)
                        {
                            Monster monster = Monster.Generate();
                                player.Fight(monster);
                        }
                        player.Walk();
                        break;
                }
            }
        }

        private static void player_LevelUp(object? sender, LevelUpEventArgs e)
        {
            /* Player player = (Player)sender; JAMAIS !!!
            * Equivalent 1
            * Player player = sender as Player;
            * if(player != null )
            * Equivalent 2
            if (sender is Player player)*/
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"--- {e.PlayerName} passe au niveau {e.Level} ---");
            Console.ResetColor();
        }
        private static void player_PotionFound(object? sender, ItemFoundEventArgs<Potion> e)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"--- {e.PlayerName} à trouvé une {e.Item} ---");
            Console.ResetColor();
        }
        private static void player_ToolsFound(object? sender, ItemFoundEventArgs<Tools> e)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"--- {e.PlayerName} à trouvé un(e) {e.Item} ---");
            Console.ResetColor();
        }
        private static void player_Trapped(object? sender, ItemFoundEventArgs<Trap> e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"--- {e.PlayerName} tombe sur un(e) {e.Item} ---");
            Console.ResetColor();
        }
        private static void player_TrapAvoided(object? sender, ItemFoundEventArgs<Trap> e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"--- {e.PlayerName} esquive le(s) {e.Item} ---");
            Console.ResetColor();
        }
        private static void player_MonsterFound(object? sender, ItemFoundEventArgs<Monster> e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"--- {e.PlayerName} tombe sur un {e.Item} ---");
            Console.WriteLine($"--- {e.PlayerName} attaque le {e.Item} ---");
            Console.ResetColor();
        }
    }
}