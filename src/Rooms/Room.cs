namespace FountainOfObjects;

public abstract class Room
{
    public abstract string Description { get; }
    public virtual ConsoleColor Color => ConsoleColor.White;
}
