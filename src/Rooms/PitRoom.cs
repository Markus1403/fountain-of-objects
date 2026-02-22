namespace FountainOfObjects;

public class PitRoom : Room
{
    public override string Description => "You fell down a pit and died.";
    public override ConsoleColor Color => ConsoleColor.DarkRed;
}
