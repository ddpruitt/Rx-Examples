using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace DC.RxExamples.SimpleCons
{
    public static class ExamplesFrom101
    {
        public static void CancelingAnAsynchronousOperation()
        {
            IObservable<int> ob =
                Observable.Create<int>(o =>
                {
                    var cancel = new CancellationDisposable(); // internally creates a new CancellationTokenSource
                    NewThreadScheduler.Default.Schedule(() =>
                    {
                        int i = 0;
                        for (;;)
                        {
                            Thread.Sleep(200); // here we do the long lasting background operation
                            if (!cancel.Token.IsCancellationRequested) // check cancel token periodically
                                o.OnNext(i++);
                            else
                            {
                                Console.WriteLine("Aborting because cancel event was signaled!");
                                o.OnCompleted();
                                return;
                            }
                        }
                    }
                        );

                    return cancel;
                }
                    );

            IDisposable subscription = ob.Subscribe(i => Console.WriteLine(i));
            Console.WriteLine("Press any key to cancel");
            Console.ReadKey();
            subscription.Dispose();
            Console.WriteLine("Press any key to quit");
            Console.ReadKey(); // give background thread chance to write the cancel acknowledge message
        }
    }
}