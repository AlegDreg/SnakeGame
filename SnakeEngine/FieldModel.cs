using System.Runtime.CompilerServices;

namespace SnakeEngine
{
    /// <summary>
    /// Описание поля
    /// </summary>
    public class FieldModel
    {
        internal PoinsTypes[,] Field { get; }
        internal int SizeX { get; }
        internal int SizeY { get; }
        private Point food;
        public FieldModel(int lenX, int lenY)
        {
            SizeY = lenY;
            SizeX = lenX;
            Field = new PoinsTypes[lenX, lenY];

            GenerateWalls();
        }

        /// <summary>
        /// Создание стен вокруг поля
        /// </summary>
        private void GenerateWalls()
        {
            for (int i = 0; i < SizeX; i++)
            {
                Field[i, 0] = PoinsTypes.Wall; //верхняя горизонтальная
                Field[i, SizeY - 1] = PoinsTypes.Wall; //нижняя горизонтальная
            }

            for (int i = 0; i < SizeY; i++)
            {
                Field[0, i] = PoinsTypes.Wall; //левая вертикальная
                Field[SizeX - 1, i] = PoinsTypes.Wall; //правая вертикальная
            }
        }

        internal void SpawnFood(int x, int y)
        {
            Field[x, y] = PoinsTypes.Food;

            if (food != null)
            {
                Field[food.X, food.Y] = PoinsTypes.Free;
            }

            food = new Point(x, y);
        }

        internal PoinsTypes this[int x, int y]
        {
            get => Field[x, y];
        }
    }
}