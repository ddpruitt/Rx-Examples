using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;

namespace DC.RxExamples.Streamers
{
    public static class OStream
    {
        public static void ObserveFileStream()
        {
            var source = new FileInfo(@".\Data\Numbers.txt");

            var stream = ObserveLines(source.FullName);
            stream.Subscribe(Console.WriteLine,             // onNext Action
                ex => Console.WriteLine("Error: {0}", ex),  // onError Action
                () => Console.WriteLine("Finished"));       // onComplete Action
        }

        public static IEnumerable<string> ReadLines(string source)
        {
            using (var reader = new StreamReader(source))
            {
                while (!reader.EndOfStream)
                    yield return reader.ReadLine();
            }
        }

        public static IObservable<string> ObserveLines(string source)
        {
            return ReadLines(source).ToObservable();
        }
    }
}