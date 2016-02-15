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
    }
}