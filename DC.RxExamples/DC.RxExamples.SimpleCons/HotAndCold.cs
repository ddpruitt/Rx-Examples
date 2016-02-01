using System;
using System.Reactive.Linq;

namespace DC.RxExamples.SimpleCons
{
    public static class HotAndCold
    {
        public static void PubConnectEscape()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();
            observable.Subscribe(i => Console.WriteLine("subscription : {0}", i));
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Press enter to connect, esc to exit.");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    var connection = observable.Connect(); //--Connects here--
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

        public static void PubConnectEscape2()
        {
            var period = TimeSpan.FromSeconds(1);
            var observable = Observable.Interval(period).Publish();

            observable.Subscribe(i => Console.Write("sub #1 : {0}", i));
            observable.Subscribe(i => Console.WriteLine("\t\tsub #2 : {0}", i));

            var exit = false;

            while (!exit)
            {
                Console.WriteLine("Press enter to connect, esc to exit.");
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    var connection = observable.Connect(); //--Connects here--
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
    }
}