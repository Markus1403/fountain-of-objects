namespace FountainOfObjects;

public class Player
{
    public int Row { get; private set; } = 0;
    public int Col { get; private set; } = 0;

    public int Arrows { get; private set; } = 5;

    public void SetPosition(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public bool Shoot(string direction, World world)
    {
        int targetRow = Row;
        int targetCol = Col;

        if (Arrows <= 0)
        {
            Console.WriteLine("You are out of arrows!");
            return false;
        }

        switch (direction)
        {
            case "shoot north":
                targetRow = Row - 1;
                break;

            case "shoot south":
                targetRow = Row + 1;
                break;

            case "shoot east":
                targetCol = Col + 1;
                break;

            case "shoot west":
                targetCol = Col - 1;
                break;

            default:
                return false;
        }

        if (!world.IsInBounds(targetRow, targetCol))
        {
            Console.WriteLine("You shoot into a wall. Nothing happens.");
            Arrows--;
            return true;
        }

        if (world.TryEliminateMonsterAt(targetRow, targetCol))
        {
            Console.WriteLine("You hear a monstrous scream! You hit something!");
        }
        else
        {
            Console.WriteLine("Nothing happened. The room must be empty");
        }

        Arrows--;
        return true;
    }

    public bool Move(string direction, World world)
    {
        int targetRow = Row;
        int targetCol = Col;

        switch (direction)
        {
            case "move north":
                targetRow--;
                break;

            case "move south":
                targetRow++;
                break;

            case "move east":
                targetCol++;
                break;

            case "move west":
                targetCol--;
                break;
            default:
                return false;
        }

        if (!world.IsInBounds(targetRow, targetCol))
        {
            Console.WriteLine("You can't move in this direction.");
            return true;
        }

        SetPosition(targetRow, targetCol);
        return true;
    }

    public void Action(string action, World world)
    {
        Room currentRoom = world.GetRoomAt(Row, Col);
        switch (action)
        {
            case "enable fountain":
                if (currentRoom is FountainRoom fountainRoom)
                {
                    if (fountainRoom.IsFountainEnabled)
                        Console.WriteLine("The fountain is already enabled!");
                    else
                        fountainRoom.EnableFountain();
                }
                else
                    Console.WriteLine("There is no fountain in this room.");
                break;

            case "disable fountain":
                if (currentRoom is FountainRoom fountainRoom2)
                {
                    if (!fountainRoom2.IsFountainEnabled)
                        Console.WriteLine("The fountain is already disabled!");
                    else
                        fountainRoom2.DisableFountain();
                }
                else
                    Console.WriteLine("There is no fountain in this room.");
                break;

            default:
                Console.WriteLine("Invalid Action!");
                break;
        }
    }

    public void Help()
    {
        Console.WriteLine("The following commands are available:");
        Console.WriteLine("1 - Move");
        Console.WriteLine("2 - Action");
        Console.WriteLine("3 - Shoot");
        Console.Write("What command do you want to learn more about? ");

        int commandNumber = Convert.ToInt32(Console.ReadLine());

        string command = commandNumber switch
        {
            1 => "Move",
            2 => "Action",
            3 => "Shoot",
            _ => "Sorry, that is not possible command",
        };

        string commandDescription = commandNumber switch
        {
            1 => @"Move:
  - Use to travel between rooms.
  - Syntax: move north | move south | move east | move west
  - Example: move north",
            2 => @"Action:
  - Use to activate or deactivate the fountain (in the fountain room).
  - Syntax: enable fountain | disable fountain
  - Example: enable fountain",
            3 => @"Shoot:
  - Use to fire an arrow into an adjacent room.
  - Syntax: shoot north | shoot south | shoot east | shoot west
  - Example: shoot east",
            _ => "Please enter 1, 2, or 3 to learn about a command.",
        };

        Console.WriteLine();
        Console.WriteLine(commandDescription);
    }
}
