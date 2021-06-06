using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace os_lab_3
{
    class Program
    {
        private const int producers = 3;
        private const int producersTimeout = 300;

        private const int consumers = 2;
        private const int consumersTimeout = 500;
        
        private const int capacity = 200;

        private static bool stop_producers = false;
        private static bool pause_producers = false;

        private static BlockingCollection<int> queue = new BlockingCollection<int>(capacity);
        
        static void Main(string[] args)
        {
            var prod_threads = new Thread[producers];
            var cons_threads = new Thread[consumers];

            for (var i = 0; i < producers; i++)
            {
                prod_threads[i] = new Thread(Produce){Name = $"Producer {i + 1}"};
                prod_threads[i].Start();
            }

            for (var i = 0; i < consumers; i++)
            {
                cons_threads[i] = new Thread(Consume){Name = $"Consumer {i + 1}"};
                cons_threads[i].Start();
            }

            do
            {
                while (!Console.KeyAvailable)
                {
                    if (queue.Count >= 100)
                        pause_producers = true;
                    if (queue.Count <= 80)
                        pause_producers = false;
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Q);

            stop_producers = true;
        }

        private static void Produce()
        {
            var rnd = new Random();

            while (true)
            {
                if (!pause_producers)
                {
                    queue.Add(rnd.Next(1, 100));
                    Console.WriteLine($"{queue.Count}: element added by {Thread.CurrentThread.Name}");
                    Thread.Sleep(producersTimeout);
                }

                if (stop_producers)
                    return;
            }
        }

        private static void Consume()
        {
            while (true)
            {
                var n = queue.Take();
                Console.WriteLine($"{queue.Count}: element taken by {Thread.CurrentThread.Name}");
                Thread.Sleep(consumersTimeout);

                if (queue.Count <= 0)
                    return;
            }
        }
    }
}