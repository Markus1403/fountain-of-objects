namespace FountainOfObjects;

public class MaelstromRoom : Room
{
    public override string Description =>
        "You entered a room with a Maelstrom, you are swept into another room";
    public override ConsoleColor Color => ConsoleColor.DarkRed;
}
