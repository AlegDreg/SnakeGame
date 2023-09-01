using SnakeEngine;

namespace Snake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var gameController = new GameController(20, 20, 100);

                while (true)
                {
                    var a = Console.ReadKey();

                    if (a.Key == ConsoleKey.Enter)
                    {
                        gameController.Dispose();
                        break;
                    }

                    bool? left = a.Key == ConsoleKey.LeftArrow ? true : a.Key == ConsoleKey.RightArrow ? false : null;

                    if (left == null)
                        continue;

                    Direction direction = gameController.Direction;
                    switch (direction)
                    {
                        case Direction.Left:
                            if ((bool)left)
                                direction = Direction.Down;
                            else
                                direction = Direction.Up;
                            break;
                        case Direction.Down:
                            if ((bool)left)
                                direction = Direction.Right;
                            else
                                direction = Direction.Left;
                            break;
                        case Direction.Right:
                            if ((bool)left)
                                direction = Direction.Up;
                            else
                                direction = Direction.Down;
                            break;
                        case Direction.Up:
                            if ((bool)left)
                                direction = Direction.Left;
                            else
                                direction = Direction.Right;
                            break;
                    }
                    gameController.SetDirection(direction);
                }
            }
        }
    }
}