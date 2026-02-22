namespace FountainOfObjects;

public class FountainRoom : Room
{
    public bool IsFountainEnabled { get; set; } = false;

    public override string Description
    {
        get
        {
            if (IsFountainEnabled == false)
                return "You hear water dripping in this room. The Fountain of Objects is here";
            else
                return "You hear water the rushing waters from the Fountain of Objects. It has been reactivated!.";
        }
    }
    public override ConsoleColor Color => ConsoleColor.DarkCyan;

    public void EnableFountain()
    {
        IsFountainEnabled = true;
    }

    public void DisableFountain()
    {
        IsFountainEnabled = false;
    }
}
