namespace SnakeEngine
{
    public interface ISnakeEngine
    {
        /// <summary>
        /// Движение вперед
        /// </summary>
        void Move();
        /// <summary>
        /// Повернуть
        /// </summary>
        /// <param name="direction"></param>
        void Turn(Direction direction);
        /// <summary>
        /// Съесть объект впереди
        /// </summary>
        void Eat(PoinsTypes point);
        /// <summary>
        /// Конец игры
        /// </summary>
        void Die();
        int SneakLen { get; }
        bool IsGameOver { get; set; }
        /// <summary>
        /// Обновление игры (тик)
        /// </summary>
        void Update();
        /// <summary>
        /// Получить результирующее поле
        /// </summary>
        /// <returns></returns>
        PoinsTypes[,] GetField();
        int Score { get; }
    }
}