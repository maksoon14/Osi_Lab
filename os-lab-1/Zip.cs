using System;
using System.IO;
using System.IO.Compression;

namespace os_lab_1
{
    static class Zip
    {
        private const string Source = "src-file.txt";
        private const string Archive = "src-file.zip";
        private const string Unzipped = "new-file-unzip.txt";
        private const int GenFileSize = 500; // bytes
        public static void Execute()
        {
            GenerateFile(GenFileSize);
            
            Compress(Source, Archive);
            Decompress(Archive, Unzipped);

            Console.WriteLine($"Source file size: {new FileInfo(Source).Length}\n" +
                              $"Archive size: {new FileInfo(Archive).Length}\n" +
                              $"Decompressed file size: {new FileInfo(Unzipped).Length}\n");
            
            File.Delete(Source);
            File.Delete(Archive);
            File.Delete(Unzipped);
            
            Console.WriteLine("Files deleted.");
        }
        
        private static void Compress(string sourcePath, string targetPath)
        {
            using (FileStream src = new FileStream(sourcePath, FileMode.OpenOrCreate))
            {
                using (FileStream target = File.Create(targetPath))
                {
                    using (GZipStream compress = new GZipStream(target, CompressionMode.Compress))
                    {
                        src.CopyTo(compress);
                    }
                }
            }
        }

        private static void Decompress(string sourcePath, string targetPath)
        {
            using (FileStream src = new FileStream(sourcePath, FileMode.OpenOrCreate))
            {
                using (FileStream target = File.Create(targetPath))
                {
                    using (GZipStream decompress = new GZipStream(src, CompressionMode.Decompress))
                    {
                        decompress.CopyTo(target);
                    }
                }
            }
        }
        private static void GenerateFile(int size)
        {
            var writer = new StreamWriter(Source);
            writer.Write(new string('E', size));
            writer.Close();
        }
    }
}
