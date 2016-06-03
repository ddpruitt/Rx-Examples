using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace DC.RxExamples.Streamers
{
    public static class RegexOnStream
    {

        public static void RegexOverFileStream()
        {
            var sourceFile = @".\Data\RostockMax01.txt";

            var regex = new Regex(@"<-T:(.*)");

            var observableFileLines = ObserveLines(sourceFile)
                .Select(s => regex.Match(s))
                .Where(m => m.Success)
                .Skip(25)
                .Take(100);

            observableFileLines.Subscribe(
                Console.WriteLine, // onNext Action
                ex => Console.WriteLine("Error: {0}", ex), // onError Action
                () => Console.WriteLine("Finished") // onComplete Action
                );
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
