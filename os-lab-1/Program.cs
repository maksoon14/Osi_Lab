using System;

namespace os_lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PrintOptions();

                int choice;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    choice = -1;
                }
                Console.Clear();
                
                switch (choice)
                {
                    case 1:
                        Drives.Execute();
                        break;
                    case 2:
                        Files.Execute();
                        break;
                    case 3:
                        Json.Execute();
                        break;
                    case 4:
                        Xml.Execute();
                        break;
                    case 5:
                        Zip.Execute();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown or invalid option");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void PrintOptions()
        {
            Console.WriteLine("1. Drives info");
            Console.WriteLine("2. Files");
            Console.WriteLine("3. JSON");
            Console.WriteLine("4. XML");
            Console.WriteLine("5. Zip archives");
            Console.Write("Choose task (1-5) or quit(0): ");
        }
    }
}
