using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace os_lab_2
{
    class Program
    {
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        public const int PasswordLength = 5;
        private const string HashesFile = "../../../hashes.txt";
        public const string DictPath = "../../../passdict.txt";
        private static int threads = Environment.ProcessorCount;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Please choose input method:");
            Console.WriteLine("1. Read hashes from file");
            Console.WriteLine("2. Read hashes from console");
            
            var choice = Convert.ToInt32(Console.ReadLine());
           
            List<string> hashes = new List<string>();
            switch (choice)
            {
                case 1:
                    hashes = ReadHashesFile(); 
                    break;
                case 2:
                    hashes = ReadHashesConsole();
                    break;
                default:
                    Console.WriteLine("Unknown option");
                    break;
            }
            
            Console.Write("Choose single-(1) or multithreading(2) mode: ");
            
            var mode = Console.ReadLine();
            
            if (mode == "2")
            {
                Console.Write($"Enter number of threads or press 'Enter' to use default value({threads}): ");
                var input = Console.ReadLine();
                if (!String.IsNullOrEmpty(input))
                    threads = Convert.ToInt32(input);
            }
            
            PassDict.GenerateDictionary();

            foreach (var hash in hashes)
            {
                var watch = Stopwatch.StartNew();
                string pass = mode switch
                {
                    "1" => HashBruteforcer.BruteForceSingle(hash),
                    "2" => HashBruteforcer.BruteForceMulti(hash, threads).Result,
                    _ => null,
                };
                watch.Stop();

                if (pass != null)
                    Console.WriteLine($"{pass}: {watch.Elapsed.Seconds}s spent cracking");
                else
                    Console.WriteLine("Password not found");
            }
            
        }

        private static List<string> ReadHashesFile()
        {
            var reader = new StreamReader(HashesFile);
            var hashes = new List<string>();
            while (!reader.EndOfStream)
                hashes.Add(reader.ReadLine());
            reader.Close();
            return hashes;
        }

        private static List<string> ReadHashesConsole()
        {
            Console.WriteLine("Enter your hashes line-by-line; Enter 0 to stop");
            var hashes = new List<string>();
            while (true)
            {
                var hash = Console.ReadLine();
                if (hash != "0")
                    hashes.Add(hash);
                else
                    break;
            }
            return hashes;
        }
    }
}