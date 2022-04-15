var movables = new Movable[500];

for (int i = 0; i < movables.Length; i++)
{
    movables[i] = new Movable();
    movables[i].Start();
}

public enum Direction
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public class Location
{
    public int X { get; set; } = 2;
    public int Y { get; set; } = 2;

    public Location(int x, int y) { X = x; Y = y; }
    public Location() { }
}

public class Movable
{
    public string Image { get; set; } = "*";
    public Location Location { get; set; } = new Location(1, 1);

    public void Start()
    {
        var thread = new Thread(() => Move());
        thread.Start();
    }

    public override string ToString()
    {
        return Image;
    }

    void Move()
    {
        while (true)
        {
            var direction = (Direction)new Random().Next(0, 4);
            switch (direction)
            {
                case Direction.Up:
                    Image = "^";
                    TransportationController.MoveUp(this);
                    break;
                case Direction.Down:
                    Image = "v";
                    TransportationController.MoveDown(this);
                    break;
                case Direction.Left:
                    Image = "<";
                    TransportationController.MoveLeft(this);
                    break;
                case Direction.Right:
                    Image = ">";
                    TransportationController.MoveRight(this);
                    break;
            }

            Thread.Sleep(100);
        }

    }
}

public static class TransportationController
{
    static object block = new object();

    public static void MoveLeft(Movable movable)
    {
        lock (block)
        {
            if (movable.Location.X <= 1)
                return;

            ClearPosition(movable);
            movable.Location.X--;
            Console.SetCursorPosition(movable.Location.X, movable.Location.Y);
            Console.Write(movable);
        }
    }

    public static void MoveRight(Movable movable)
    {
        lock (block)
        {
            if (movable.Location.X >= Console.WindowWidth - 2)
                return;

            ClearPosition(movable);
            movable.Location.X++;
            Console.SetCursorPosition(movable.Location.X, movable.Location.Y);
            Console.Write(movable);
        }
    }
    public static void MoveDown(Movable movable)
    {
        lock (block)
        {
            if (movable.Location.Y >= Console.WindowHeight - 2)
                return;

            ClearPosition(movable);
            movable.Location.Y++;
            Console.SetCursorPosition(movable.Location.X, movable.Location.Y);
            Console.Write(movable);
        }
    }
    public static void MoveUp(Movable movable)
    {
        lock (block)
        {
            if (movable.Location.Y <= 1)
                return;

            ClearPosition(movable);
            movable.Location.Y--;
            Console.SetCursorPosition(movable.Location.X, movable.Location.Y);
            Console.Write(movable);
        }
    }

    static void ClearPosition(Movable movable)
    {
        Console.SetCursorPosition(movable.Location.X, movable.Location.Y);
        Console.Write(" ");
    }
}