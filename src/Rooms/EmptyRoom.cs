namespace FountainOfObjects;

public class EmptyRoom : Room
{
    public override string Description => "This room is empty and there is nothing to see.";
    public override ConsoleColor Color => ConsoleColor.DarkGray;
}
