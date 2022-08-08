var snake = new Snake(30);

while (true)
{
    var keyInfo = Console.ReadKey();

    switch (keyInfo.Key)
    {
        case ConsoleKey.LeftArrow:
            snake.Move(Movable.Direction.Left);
            break;
        case ConsoleKey.RightArrow:
            snake.Move(Movable.Direction.Right);
            break;
        case ConsoleKey.UpArrow:
            snake.Move(Movable.Direction.Up);
            break;
        case ConsoleKey.DownArrow:
            snake.Move(Movable.Direction.Down);
            break;
    }
}

class Snake : Movable
{   
    public List<SnakeBlock> Body { get; set; }
    public SnakeBlock Head { get; private set; }
    public SnakeBlock Tail { get; private set; }

    class BreakPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        public BreakPoint() { }
        public BreakPoint(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }

    private List<BreakPoint> breakPoints = new();

    public Snake(int length = 5)
    {
        Body = new List<SnakeBlock>();

        for (int i = length - 1; i > -1; i--)
            Body.Add(new SnakeBlock() { X = i, Y = 0 });

        Head = Body[0];
        Tail = Body.Last();

        Body.ForEach(b => b.ObjectDirection = Direction.Right);
        Body.ForEach(b => Console.Write(b));
    }

    new public void Move(Direction direction)
    {
        foreach (var block in Body)
        {
            Console.SetCursorPosition(block.X, block.Y);
            Console.Write(" ");
        }

        if (Head.ObjectDirection != direction)
        {
            breakPoints.Add(new BreakPoint(Head.X, Head.Y, direction));
        }

        foreach(var block in Body)
        {
            var queue = new Queue<BreakPoint>();
            var wasMoved = false;

            breakPoints.ForEach(b => queue.Enqueue(b));

            while (queue.Count != 0)
            {
                var breakPoint = queue.Dequeue();

                if (breakPoint.X == block.X && breakPoint.Y == block.Y)
                {
                    if (block.Equals(Tail))
                        breakPoints.Remove(breakPoint);

                    block.ObjectDirection = direction;
                    block.Move(breakPoint.Direction);
                    wasMoved = true;
                }
            }
            if (!wasMoved)
                block.Move();
        }

        foreach(var block in Body)
        {
            Console.SetCursorPosition(block.X, block.Y);
            Console.Write(block);
        }
    }
}

class SnakeBlock : Movable
{
    public bool Visible { get; set; } = true;
    public string Image { get; set; } = "*";

    public override string ToString()
    {
        return Image;
    }
}

abstract class Movable
{
    public int X { get; set; }
    public int Y { get; set; }

    public enum Direction
    {
        NoDirection = 0,
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4
    }

    public Direction ObjectDirection { get; set; }

    public virtual void Move(Direction direction = Direction.NoDirection)
    {
        if (direction != Direction.NoDirection)
            ObjectDirection = direction;

        switch (ObjectDirection)
        {
            case Direction.Up:
                Y -= 1;
                break;
            case Direction.Down:
                Y += 1;
                break;
            case Direction.Left:
                X -= 1;
                break;
            case Direction.Right:
                X += 1;
                break;
        }
    }
}