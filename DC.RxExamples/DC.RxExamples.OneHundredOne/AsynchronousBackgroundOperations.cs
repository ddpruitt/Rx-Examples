using System;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace DC.RxExamples.OneHundredOne
{
    /// <summary>
    /// Asynchronous Background Operations
    /// </summary>
    public static class AsynchronousBackgroundOperations
    {
        /// <summary>
        /// Start - Run Code Asynchronously
        /// </summary>
        public static void StartBacgroundWork()
        {
            Console.WriteLine("Shows use of Start to start on a bacground thread:");
            var obs = Observable.Start(() =>
            {
                //This starts on background thread.
                Console.WriteLine("Hello from background, not blocking the main thread.");
                Console.WriteLine("Now we sleep...");
                Thread.Sleep(3000);
                Console.WriteLine("BG work complete, wakey wake.");
            })
                .Finally(() => Console.WriteLine("Main thread completed."));

            Console.WriteLine("\r\n\tIn Main Thread...\r\n");

            obs.Wait();  // wait for completion of background operation.
        }

        /// <summary>
        /// Run a method asynchronoulsy on demand
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int DoLongRunningOperation(string param)
        {
            Console.WriteLine("Long Running Operation on Thread: {0}, Param {1}", 
                Thread.CurrentThread.ManagedThreadId, param);
            Thread.Sleep(1000);
            return string.IsNullOrWhiteSpace(param) ? 0 : param.Length;
        }

        public static IObservable<int> LongRunningOperationAsync(string param)
        {
            return Observable.Create<int>(
                o => Observable.ToAsync<string, int>(DoLongRunningOperation)(param).Subscribe(o));
        }

        /// <summary>
        /// CombineLatest - Parallel Execution
        /// </summary>
        public static async void ParallelExecutionTest()
        {
            var o = Observable.CombineLatest(
                Observable.Start(() =>
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("Executing 1st on Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    return "Result A";
                }),
                Observable.Start(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Executing 2nd on Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    return "Result B";
                }),
                Observable.Start(() =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Executing 3rd on Thread: {0}", Thread.CurrentThread.ManagedThreadId);
                    return "Result C";
                })
                )
                .Finally(() => Console.WriteLine("Done!"));

            Console.WriteLine("Now we wait...");
            Thread.Sleep(4000);

            foreach (var r in await o.FirstAsync())
                Console.WriteLine("Waited for: {0}", r);
        }
    }
}