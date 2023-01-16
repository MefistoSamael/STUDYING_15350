namespace Entities;


public class Progress : IProgress<int>
{
    public event Action<int>? Action;

    public void Report(int value)
    {
        Action?.Invoke(value);
    }
}
