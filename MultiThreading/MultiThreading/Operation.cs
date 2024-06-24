namespace MultiThreading;

public delegate void ReturtDataDelegate(int result);
public class Operation
{
    private object lockObject = new object();
    public int AppleCount = 6;

    public int GetAndKarsillatibEat(int count, int waitedMinutes)
    {
        lock(lockObject)
        {
            if (AppleCount >= count)
            {
                Thread.Sleep(waitedMinutes);
                AppleCount -= count;
                return count;
            }

            return AppleCount;
        }
    }
}
