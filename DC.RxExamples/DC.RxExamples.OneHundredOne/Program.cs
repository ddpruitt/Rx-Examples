using System;
using System.Reactive.Linq;
using System.Threading;

namespace DC.RxExamples.OneHundredOne
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Main Thread {0} - Start Background Work", Thread.CurrentThread.ManagedThreadId);

            AsynchronousBackgroundOperations.StartBackgroundWork();

            Console.WriteLine("Main Thread {0} - Start Long Running Operations", Thread.CurrentThread.ManagedThreadId);

            AsynchronousBackgroundOperations.LongRunningOperationAsync("Test String").Wait();
            AsynchronousBackgroundOperations.LongRunningOperationAsync("Test String 2").Wait();
            AsynchronousBackgroundOperations.LongRunningOperationAsync("Test String 3").Wait();

            Console.WriteLine("Main Thread {0} - Start Parallel Executions", Thread.CurrentThread.ManagedThreadId);
            AsynchronousBackgroundOperations.ParallelExecutionTest();

            Console.WriteLine("Main Thread {0} - Start Disposable Scheduler", Thread.CurrentThread.ManagedThreadId);
            AsynchronousBackgroundOperations.DisposableScheduler();
        }
    }
}
