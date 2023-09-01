using System;

namespace SnakeEngine
{
    public class Engine : ISnakeEngine
    {
        private SnakeModel SnakeModel { get; }
        private FieldModel FieldModel { get; }
        public bool IsGameOver { get; set; }
        public int SneakLen => SnakeModel.SneakLen;

        public int Score => SnakeModel.SneakLen - 1;

        /// <summary>
        /// Была ли съедена еда. Если да - один ход последний участок тела не движется
        /// </summary>
        private bool isEatEffect = false;
        private Direction? _direction;

        public Engine(SnakeModel snakeModel, FieldModel fieldModel)
        {
            SnakeModel = snakeModel;
            FieldModel = fieldModel;

            RandomDirection();
            SpawnFood();
        }

        private void RandomDirection()
        {
            Random random = new Random();
            var l = GetTypes<Direction>();
            SnakeModel.SetDirection(l[random.Next(0, l.Count - 1)]);
        }

        public void Die()
        {
            IsGameOver = true;
        }

        public void Eat(PoinsTypes point)
        {
            isEatEffect = true;
            SnakeModel.AddBody(10);

            SpawnFood();
        }

        private void SpawnFood()
        {
            Random random = new Random();
            FieldModel.SpawnFood(random.Next(1, FieldModel.SizeX - 1), random.Next(1, FieldModel.SizeY - 1));
        }

        public List<T> GetTypes<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public void Move()
        {
            if (IsGameOver)
                return;

            // если нет эффекта еды, то хвост движется
            if (!isEatEffect && SnakeModel.Body.Count > 0)
            {
                if (SnakeModel.Body.Count > 1)
                {
                    SnakeModel.Body[SnakeModel.Body.Count - 1].X = SnakeModel.Body[SnakeModel.Body.Count - 2].X;
                    SnakeModel.Body[SnakeModel.Body.Count - 1].Y = SnakeModel.Body[SnakeModel.Body.Count - 2].Y;
                }
                else
                {
                    SnakeModel.Body[SnakeModel.Body.Count - 1].X = SnakeModel.Head.X;
                    SnakeModel.Body[SnakeModel.Body.Count - 1].Y = SnakeModel.Head.Y;
                }
            }

            // движение тела на клетку следующего участка
            for (int i = SnakeModel.Body.Count - 1; i > 0; i--)
            {
                SnakeModel.Body[i].X = SnakeModel.Body[i - 1].X;
                SnakeModel.Body[i].Y = SnakeModel.Body[i - 1].Y;
            }

            // движение первого участка тела на место головы
            if (SnakeModel.Body.Count > 0)
            {
                SnakeModel.Body[0].X = SnakeModel.Head.X;
                SnakeModel.Body[0].Y = SnakeModel.Head.Y;
            }

            // движение головы на клетку вперед
            int x = SnakeModel.Head.X;
            int y = SnakeModel.Head.Y;
            switch (SnakeModel.Direction)
            {
                case Direction.Up:
                    y++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
            }
            SnakeModel.Head.X = x;
            SnakeModel.Head.Y = y;


            isEatEffect = false;
        }

        public void Turn(Direction direction)
        {
            _direction = direction;
        }

        private void Rotate()
        {
            if (_direction != null)
            {
                SnakeModel.SetDirection((Direction)_direction);
                _direction = null;
            }
        }

        private PoinsTypes CheckAhead()
        {
            int x = SnakeModel.Head.X;
            int y = SnakeModel.Head.Y;
            switch (SnakeModel.Direction)
            {
                case Direction.Up:
                    y++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
            }
            return CheckPoint(x, y);
        }

        public void Update()
        {
            Rotate();

            var point = CheckAhead();

            Move();

            if (point == PoinsTypes.Food)
                Eat(point);
            else if (point == PoinsTypes.Wall || point == PoinsTypes.Sneak)
                Die();
        }

        public PoinsTypes CheckPoint(int x, int y)
        {
            if (SnakeModel.HasSnaeakAtPoint(x, y))
                return PoinsTypes.Sneak;

            return FieldModel[x, y];
        }

        public PoinsTypes[,] GetField()
        {
            PoinsTypes[,] field = new PoinsTypes[FieldModel.SizeX, FieldModel.SizeY];
            for (int i = 0; i < FieldModel.SizeX; i++)
            {
                for (int j = 0; j < FieldModel.SizeY; j++)
                {
                    if (SnakeModel.HasSnaeakAtPoint(i, j))
                    {
                        field[i, j] = PoinsTypes.Sneak;
                    }
                    else
                    {
                        field[i, j] = FieldModel[i, j];
                    }
                }
            }

            return field;
        }
    }
}
