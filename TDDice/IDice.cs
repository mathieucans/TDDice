namespace TDDice
{
    public interface IDice
    {
        void Roll();

        int Value { get; }
    }
}
