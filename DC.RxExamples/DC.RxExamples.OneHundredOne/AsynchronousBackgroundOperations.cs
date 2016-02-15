using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace DC.RxExamples.OneHundredOne
{
    public static class AsynchronousBackgroundOperations
    {
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

        // Run a method asynchronoulsy on demand
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
    }
}