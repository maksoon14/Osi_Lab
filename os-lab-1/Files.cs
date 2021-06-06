using System;
using System.IO;

namespace os_lab_1
{
    static class Files
    {
        private const string Path = "test-file.txt";
        public static void Execute()
        {
            var writer = new StreamWriter(Path);
            Console.WriteLine("Enter a string to be written to file: ");
            var content = Console.ReadLine();
            writer.WriteLine(content);
            writer.Close();

            var reader = new StreamReader(Path);
            Console.WriteLine($"File opened: {Path}");
            Console.Write($"Contents: {reader.ReadToEnd()}");
            reader.Close();

            File.Delete(Path);
            Console.WriteLine("File deleted");
        }
    }
}
