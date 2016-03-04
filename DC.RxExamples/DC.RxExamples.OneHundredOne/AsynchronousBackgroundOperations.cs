using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
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
        public static void StartBackgroundWork()
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

            Console.WriteLine("\n\nDone with background threads.");

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
            Console.WriteLine("\nLong running operation async");
            return Observable.Create<int>(
                o => Observable.ToAsync<string, int>(DoLongRunningOperation)(param).Subscribe(o));
        }

        /// <summary>
        /// CombineLatest - Parallel Execution
        /// </summary>
        public static async void ParallelExecutionTest()
        {
            Console.WriteLine("\n\nParallel Executeion of A, B and C.");
            var o = Observable.CombineLatest(
                Observable.Start(() =>
                {
                    Console.WriteLine("Start Executing 1st on Thread (A): {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(3000);
                    Console.WriteLine("Done Executing 1st on Thread (A): {0}", Thread.CurrentThread.ManagedThreadId);
                    return "Result A";
                }),
                Observable.Start(() =>
                {
                    Console.WriteLine("Start Executing 2nd on Thread (B): {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(2000);
                    Console.WriteLine("Done Executing 2nd on Thread (B): {0}", Thread.CurrentThread.ManagedThreadId);
                    return "Result B";
                }),
                Observable.Start(() =>
                {
                    Console.WriteLine("Start Executing 3rd on Thread (C): {0}", Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(1000);
                    Console.WriteLine("Done Executing 3rd on Thread (C): {0}", Thread.CurrentThread.ManagedThreadId);
                    return "Result C";
                })
                )
                .Finally(() => Console.WriteLine("Done!"));

            Console.WriteLine("Now we wait...");
            Thread.Sleep(4000);

            foreach (var r in await o.FirstAsync())
                Console.WriteLine("Waited for: {0}", r);

            Console.WriteLine("\n\nComplete Parallel Executeion of A, B and C.");

        }

        /// <summary>
        /// Create With Disposable & Scheduler - Canceling an asynchronous operation
        /// </summary>
        public static void DisposableScheduler()
        {
            IObservable<int> ob =
                Observable.Create<int>(o =>
                {
                    var cancel = new CancellationDisposable();
                    NewThreadScheduler.Default.Schedule(() =>
                    {
                        int i = 0;
                        while (!cancel.Token.IsCancellationRequested)
                        {
                            Thread.Sleep(200);
                            o.OnNext(i++);
                        }

                        Console.WriteLine("Aborting because cancel evente was signled.");
                        o.OnCompleted();
                    });
                    return cancel;
                });

            IDisposable subscription = ob.Subscribe(i => Console.WriteLine("\t{0:000}",i));
            Console.WriteLine("Press any key to cancel");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("Background should be cleaned up by now, Press any key to quit");
            Console.ReadKey(); // give background thread chance to write cancel message
        }
    }
}