namespace MultiThreading;

internal class Program
{
    public delegate int Nimadir(int son1, int son2);
    static void Main(string[] args)
    {
        /// Main Thread
        Thread.CurrentThread.Name = "Main Thread";

        Console.WriteLine(Thread.CurrentThread.Name + "is started");

        //Console.WriteLine("Method Main is started");


        // ThreadStart threadStart = new ThreadStart(MethodA);
        // ThreadStart threadStart = MethodA;

        Operation operation = new Operation(
            10,
            new ReturtDataDelegate(
                (int natija) =>
                {
                    Console.WriteLine($"Yig'indi = {natija}");
                }));
        
        ThreadStart parameterizedThreadStart = new ThreadStart(operation.Method);

        ReturtDataDelegate returtDataDelegate = delegate (int natija)
        {
            Console.WriteLine($"Yig'indi {natija}");
        };
        ThreadStart threadStart = new Operation(15, returtDataDelegate).Method;
        Thread workerThread1 = new Thread(parameterizedThreadStart)
        {
            IsBackground = true,
            Name = "Worker Thread 1"
        };

        Thread workerThread2 = new Thread(new Operation(20, ReturnMethod).Method)
        {
            IsBackground = true,
            Name = "Worker Thread 2"
        };

        Thread workerThraed3 = new Thread(threadStart)
        {

            IsBackground= fa,
            Name = "Worker thread 3"
        };

        workerThread1.Start();
        workerThread2.Start();
        workerThraed3.Start();

        //Console.WriteLine("Method Main is ended");

        Console.WriteLine(Thread.CurrentThread.Name + "is ended");

    }

    public static void ReturnMethod(int natija)
    {
        Console.WriteLine($"Yig'indi natija : {natija}");
    }

    //static void MethodA(object? number)
    //{
    //    int son = (int)number;
    //    for(int i = 0; i < son; i++)
    //    {
    //        Console.WriteLine(Thread.CurrentThread.Name + "   " + i);
    //        Thread.Sleep(500);
    //    }
    //}

    //static void MethodB(object? number)
    //{
    //    int son = (int)number;  
    //    for (int i = 0; i < son; i++)
    //    {
    //        Console.WriteLine(Thread.CurrentThread.Name + "   " + i);
    //        Thread.Sleep(500);
    //    }
    //}
    
    //static void MethodC(object? number)
    //{
    //    int son = (int)number;
    //    for (int i = 0; i < son; i++)
    //    {
    //        Console.WriteLine(Thread.CurrentThread.Name + "   " + i);
    //        Thread.Sleep(500);
    //    }
    //}
}
