namespace FountainOfObjects;

public class AmarokRoom : Room
{
    public override string Description =>
        "You entered a room with an Amarok, before you could even react it ripped you apart";
    public override ConsoleColor Color => ConsoleColor.DarkRed;
}
