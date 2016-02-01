using System;
using System.IO;
using System.Reactive.Linq;
using System.Text;

namespace DC.RxExamples.SimpleCons
{
    public static class Orange
    {
        /// <summary>
        /// Simple Observable int range, written to the Console.
        /// Note:  This is not async
        /// </summary>
        public static void FirstObservableRange()
        {
            Console.WriteLine("First Observable Range Example");
            Console.WriteLine(new string('-', 25));

            // Some input to observe
            var input = Observable.Range(1, 25);


            // It is IDisposable so wrap in using.
            using (var sub = input.Subscribe(
                Console.WriteLine,                          // onNext Action
                ex => Console.WriteLine("Error: {0}", ex),  // onError Action
                () => Console.WriteLine("Finished")))       // onComplete Action
            {
                // We don't actually do anything.
            }

            Console.WriteLine(new string('-', 25));
        }

        public static void WriteRangeToFile()
        {
            Observable.Using(
                () => new FileStream(@".\WriteRangeToFile.txt", FileMode.Create),
                fs => Observable.Range(0, 10000).Select(v => Encoding.ASCII.GetBytes(v.ToString()))
                )
                .Subscribe();

        }
    }
}