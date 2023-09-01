namespace SnakeEngine
{
    /// <summary>
    /// Описание змеи
    /// </summary>
    public class SnakeModel
    {
        /// <summary>
        /// Длина змеи
        /// </summary>
        internal int SneakLen => Body.Count + 1;
        /// <summary>
        /// Голова
        /// </summary>
        internal Point Head { get; }
        /// <summary>
        /// Туловище (включая хвост)
        /// </summary>
        internal List<Point> Body { get; private set; }
        /// <summary>
        /// Хвост
        /// </summary>
        internal Point Tail
        {
            get
            {
                if (Body.Count > 0)
                {
                    return Body[Body.Count - 1];
                }
                else
                {
                    return Head;
                }
            }
        }

        /// <summary>
        /// Направление движения
        /// </summary>
        public Direction Direction { get; private set; }

        internal void SetDirection(Direction direction)
        {
            Direction = direction;
        }

        public SnakeModel(int headX, int headY)
        {
            Head = new Point(headX, headY);
            Body = new List<Point>();
        }

        internal void AddBody(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (Body.Count == 0)
                {
                    Body.Add(new Point(Head.X, Head.Y));
                }
                else
                {
                    Body.Add(new Point(Body[Body.Count - 1].X, Body[Body.Count - 1].Y));
                }
            }
        }

        /// <summary>
        /// Есть ли змея в точке
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal bool HasSnaeakAtPoint(int x, int y)
        {
            if (Head.X == x && Head.Y == y)
                return true;
            for (int i = 0; i < Body.Count; i++)
            {
                if (Body[i].X == x && Body[i].Y == y)
                    return true;
            }

            return false;
        }
    }

    internal class Point
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}