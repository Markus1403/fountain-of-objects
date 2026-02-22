namespace FountainOfObjects;

public class World
{
    private readonly Room[,] grid;
    public int Rows => grid.GetLength(0);
    public int Cols => grid.GetLength(1);
    public FountainRoom FountainRoom { get; }

    public World()
    {
        grid = new Room[4, 4];

        for (int row = 0; row < 4; row++)
        for (int col = 0; col < 4; col++)
            grid[row, col] = new EmptyRoom();

        grid[0, 0] = new StartingRoom();

        FountainRoom = new FountainRoom();
        grid[3, 3] = FountainRoom;

        grid[3, 1] = new PitRoom();

        grid[1, 2] = new MaelstromRoom();

        grid[0, 3] = new AmarokRoom();
    }

    public Room GetRoomAt(int row, int col)
    {
        return grid[row, col];
    }

    public bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < Rows && col >= 0 && col < Cols;
    }

    public bool TryEliminateMonsterAt(int row, int col)
    {
        if (!IsInBounds(row, col))
            return false;

        if (grid[row, col] is AmarokRoom || grid[row, col] is MaelstromRoom)
        {
            grid[row, col] = new EmptyRoom();
            return true;
        }

        return false;
    }

    public bool ResolveMaelstromEncounter(Player player)
    {
        if (GetRoomAt(player.Row, player.Col) is not MaelstromRoom)
            return false;

        int oldMaelstromRow = player.Row;
        int oldMaelstromCol = player.Col;

        int newPlayerRow = Mod(player.Row - 1, Rows);
        int newPlayerCol = Mod(player.Col + 2, Cols);
        player.SetPosition(newPlayerRow, newPlayerCol);

        int newMaelstromRow = Mod(oldMaelstromRow + 1, Rows);
        int newMaelstromCol = Mod(oldMaelstromCol - 2, Cols);

        grid[oldMaelstromRow, oldMaelstromCol] = new EmptyRoom();
        grid[newMaelstromRow, newMaelstromCol] = new MaelstromRoom();
        return true;
    }

    private static int Mod(int value, int modulo)
    {
        return ((value % modulo) + modulo) % modulo;
    }

    public bool IsRoomTypeAdjacent<T>(int row, int col)
        where T : Room
    {
        int[,] directions = new int[,]
        {
            { -1, 0 },
            { -1, 1 },
            { 0, 1 },
            { 1, 1 },
            { 1, 0 },
            { 1, -1 },
            { 0, -1 },
            { -1, -1 },
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newRow = row + directions[i, 0];
            int newCol = col + directions[i, 1];

            if (
                newRow >= 0
                && newRow < Rows
                && newCol >= 0
                && newCol < Cols
            )
            {
                if (grid[newRow, newCol] is T)
                    return true;
            }
        }
        return false;
    }
}
