namespace LibraryLab8
{
    public class Progress : IProgress<int>
    {
        public event Action<int>? progress;

        public void Report(int value)
        {
            progress?.Invoke(value);
        }
    }
}
