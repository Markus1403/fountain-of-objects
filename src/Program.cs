namespace FountainOfObjects;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "The Fountain of Objects";
        Game game = new Game();
        game.GameRunning();
    }
}
