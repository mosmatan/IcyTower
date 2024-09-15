namespace Assets.Scripts
{
    public interface IJumper
    {
        float MaxHeight { get; }

        float CurrentHeight { get; }
        void Jump();
        void Move(int direction);
    }
}
