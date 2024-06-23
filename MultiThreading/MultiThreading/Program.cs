using System.ComponentModel;
using System.Diagnostics;

namespace MultiThreading;

internal class Program
{
    static void Main(string[] args)
    {
        /// Main Thread
        /// 
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        for (int i = 0;i < 1000; i ++)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(PrintNumbers));
          
        }

        stopwatch.Stop();

        Console.WriteLine("thread pool : " + stopwatch.ElapsedTicks);

        stopwatch.Restart();
        for (long i = 0; i < 1000; i++)
        {
           Thread thread = new Thread(PrintNumbers);
            thread.Start();

        }
        stopwatch.Stop();

        Console.WriteLine("thread with new " + stopwatch.ElapsedTicks);

        Console.ReadLine();

    }

    static void PrintNumbers(object? parametr)
    {

        //Console.WriteLine($"{Thread.CurrentThread.Name} {Thread.CurrentThread.ManagedThreadId}");
        //Thread.Sleep(2000);

        long sum = 0;
        for(int i = 0;i < 10_000; i ++)
        {
            sum += i;
        }
        
    }

   
}
