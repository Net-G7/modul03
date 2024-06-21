namespace MultiThreading;

public delegate void ReturtDataDelegate(int result);
public class Operation
{
    public ReturtDataDelegate ReturtData;
    public Operation(int number, ReturtDataDelegate returtData)
    {
        Number = number;
        ReturtData = returtData;
    }

    public int Number { get; set; }


    public void Method()
    {
        int sum = 0;
        for (int i = 0; i < Number; i++)
        {
            Console.WriteLine(Thread.CurrentThread.Name + "   " + i);
            sum += i;
            Thread.Sleep(500);
        }

        ReturtData.Invoke(sum);
    }
}
