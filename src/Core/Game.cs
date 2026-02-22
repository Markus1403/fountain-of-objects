namespace FountainOfObjects;

public class Game
{
    private World world;
    private Player player;

    private TimeSpan elaspedTime;

    private DateTime startTime;
    private DateTime endTime;

    public Game()
    {
        world = new World();
        player = new Player();
        startTime = DateTime.UtcNow;
    }

    public void GameRunning()
    {
        Console.Clear();
        string[] introLines = new[]
        {
            "You enter the Caverns of Objects, a maze of rooms filled",
            "with dangerous pits, in search of the Fountain of Objects.",
            "",
            "Light is visible only in the entrance; no other light is",
            "seen anywhere in the caverns.",
            "",
            "You must navigate the caverns with your other senses.",
            "",
            "Find the Fountain of Objects, activate it, and return to",
            "the entrance.",
            "",
            "Look out for pits. You feel a breeze if a pit is in an",
            "adjacent room. If you enter a room with a pit, you will die.",
            "",
            "Maelstroms are violent forces of sentient wind. Entering",
            "a room with one could transport you to another location.",
            "You will hear their growling and groaning in nearby rooms.",
            "",
            "Amaroks roam the caverns. Encountering one is certain death,",
            "but you can smell their rotten stench in nearby rooms.",
            "",
            "You carry a bow and a quiver of arrows. Use them to shoot",
            "monsters, but beware: your supply is limited.",
        };

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(new string('=', 60));
        foreach (var line in introLines)
            Console.WriteLine(line.PadLeft((60 + line.Length) / 2));
        Console.WriteLine(new string('=', 60));
        Console.ResetColor();
        Console.WriteLine();

        while (!IsGameWon() && !IsGameLost())
        {
            Console.WriteLine("---------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"You are in the room at (Row={player.Row}, Col={player.Col})");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Total Arrows: {player.Arrows}");
            Console.ResetColor();

            Room currentRoom = world.GetRoomAt(player.Row, player.Col);
            Console.ForegroundColor = currentRoom.Color;
            Console.WriteLine(currentRoom.Description);
            Console.ResetColor();

            if (world.IsRoomTypeAdjacent<PitRoom>(player.Row, player.Col))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("You feel a draft. There is a pit in a nearby room.");
                Console.ResetColor();
            }

            if (world.IsRoomTypeAdjacent<MaelstromRoom>(player.Row, player.Col))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("You hear the growling and groaning of a maelstrom nearby");
                Console.ResetColor();
            }

            if (world.IsRoomTypeAdjacent<AmarokRoom>(player.Row, player.Col))
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("You can smell the rotten stench of an amarok in a nearby room");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("What do you want to do? ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? input = Console.ReadLine();
            if (input != null)
            {
                if (input.Equals("help"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    player.Help();
                    Console.WriteLine("\nPress Enter to return to the game...");
                    Console.ReadLine();
                    Console.Clear();
                    Console.ResetColor();
                    continue; // Skip the rest of the loop and show the room again
                }
                if (input.StartsWith("move"))
                {
                    player.Move(input, world);
                }
                else if (input.StartsWith("shoot"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    player.Shoot(input, world);
                    Console.ResetColor();
                }
                else
                {
                    player.Action(input, world);
                }
                Room roomAfterMove = world.GetRoomAt(player.Row, player.Col);
                if (roomAfterMove is MaelstromRoom)
                {
                    Console.ForegroundColor = roomAfterMove.Color;
                    Console.WriteLine(roomAfterMove.Description);
                    Console.ResetColor();
                    world.ResolveMaelstromEncounter(player);
                }
            }
            Console.ResetColor();
        }
    }

    public bool IsGameWon()
    {
        Room currentRoom = world.GetRoomAt(player.Row, player.Col);
        if (currentRoom is StartingRoom && world.FountainRoom.IsFountainEnabled == true)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                "The Fountain of Objects has been reactivated, and you escaped with your life!"
            );
            Console.WriteLine("You Win!");
            TimeElapsed();
            return true;
        }
        return false;
    }

    public bool IsGameLost()
    {
        Room currentRoom = world.GetRoomAt(player.Row, player.Col);
        if (currentRoom is PitRoom || currentRoom is AmarokRoom)
        {
            Console.ForegroundColor = currentRoom.Color;
            Console.WriteLine(currentRoom.Description);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("You Lost!");
            TimeElapsed();
            return true;
        }
        return false;
    }

    private void TimeElapsed()
    {
        endTime = DateTime.UtcNow;
        elaspedTime = endTime - startTime;
        Console.WriteLine(
            $"Time elapsed: {elaspedTime.Hours}h {elaspedTime.Minutes}m {elaspedTime.Seconds}s {elaspedTime.Milliseconds}ms"
        );
    }
}
