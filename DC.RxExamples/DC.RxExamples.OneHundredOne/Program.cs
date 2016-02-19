using System;
using System.Reactive.Linq;
using System.Threading;

namespace DC.RxExamples.OneHundredOne
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Main Thread {0}", Thread.CurrentThread.ManagedThreadId);

            AsynchronousBackgroundOperations.StartBacgroundWork();

            Console.WriteLine("Main Thread {0}", Thread.CurrentThread.ManagedThreadId);

            AsynchronousBackgroundOperations.LongRunningOperationAsync("Test String").Wait();
            AsynchronousBackgroundOperations.LongRunningOperationAsync("Test String 2").Wait();
            AsynchronousBackgroundOperations.LongRunningOperationAsync("Test String 3").Wait();

            Console.WriteLine("Main Thread {0}", Thread.CurrentThread.ManagedThreadId);
            AsynchronousBackgroundOperations.ParallelExecutionTest();
        }
    }
}
