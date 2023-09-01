namespace SnakeEngine
{
    public class GameController
    {
        public FieldModel FieldModel { get; private set; }
        public SnakeModel SnakeModel { get; private set; }
        ISnakeEngine Engine;
        Print Print;
        System.Timers.Timer Timer;

        public GameController(int sizeX, int sizeY, int millisec)
        {
            Random random = new Random();

            Print = new Print();

            FieldModel = new FieldModel(sizeX, sizeY);
            SnakeModel = new SnakeModel(sizeX / 2, sizeY / 2);
            Engine = new Engine(SnakeModel, FieldModel);

            Timer = new System.Timers.Timer(millisec);
            Timer.Elapsed += Timer_Elapsed;
            Timer.AutoReset = true;
            Timer.Start();
        }

        public Direction Direction => SnakeModel.Direction;

        public void SetDirection(Direction direction)
        {
            Engine.Turn(direction);
        }

        public void Dispose()
        {
            Timer.Elapsed -= Timer_Elapsed;
            try
            {
                Timer.Dispose();
            }
            catch { }
            Print.Close();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Engine.Update();
            Print.Display(Engine.GetField(), Engine);
        }
    }
}