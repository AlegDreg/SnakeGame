namespace SnakeEngine
{
    internal class Print
    {
        private bool isPrintedEnd;

        internal Print()
        {
            Console.CursorVisible = false;
        }

        internal void Close()
        {
            Console.Clear();
        }

        internal void Display(PoinsTypes[,] field, ISnakeEngine engine)
        {
            if (!engine.IsGameOver)
            {
                Console.Write(new string(' ', field.GetLength(0) + 5));
                Console.Write("Очков - " + engine.Score);

                Console.CursorLeft = 0;
                Console.CursorTop = 0;

                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        SetColor(field[i, j]);
                        Console.Out.Write("0");
                    }
                    Console.Out.Write('\n');
                }

                Console.CursorLeft = 0;
                Console.CursorTop = 0;
            }

            if (engine.IsGameOver && !isPrintedEnd)
            {
                isPrintedEnd = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.CursorTop = field.GetLength(1) + 3;
                Console.Out.WriteLine($"\nКонец игры. Длина змеи - {engine.SneakLen}. Enter - ещё раз");
            }
        }

        private void SetColor(PoinsTypes color)
        {
            switch (color)
            {
                case PoinsTypes.Free:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case PoinsTypes.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case PoinsTypes.Sneak:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case PoinsTypes.Food:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }
    }
}