using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;

namespace DC.RxExamples.Streamers
{
    public class RostockMax
    {
        public FileInfo RostockMax01 { get; set; } = new FileInfo(@".\Data\RostockMax01.txt");

        public void HotReadFileToConsole()
        {
            var publishedStream = ReadLines(RostockMax01)
                .ToObservable<string>()
                .Publish();

            publishedStream.Subscribe(Console.WriteLine);

            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Press enter to connect, esc to exit.");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    var connection = publishedStream.Connect(); //--Connects here--
                    Console.WriteLine("Press any key to dispose of connection.");
                    Console.ReadKey();
                    connection.Dispose(); //--Disconnects here--
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
            }
        }

        public IEnumerable<string> ReadLines(FileInfo sourceFile)
        {
            using (var reader = new StreamReader(sourceFile.FullName))
            {
                while (true)
                {
                    if (reader.EndOfStream)
                    {
                        reader.BaseStream.Position = 0;
                        reader.DiscardBufferedData();
                    }
                    yield return reader.ReadLine();
                }
            }
        }

       
    }
}