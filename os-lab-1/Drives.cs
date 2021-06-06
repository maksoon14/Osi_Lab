using System;
using System.IO;

namespace os_lab_1
{
    static class Drives
    {
        public static void Execute()
        {
            var drives = DriveInfo.GetDrives();

            Console.WriteLine($"{drives.Length} drives detected\n");
            foreach(var drive in drives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Name: {drive.Name}");
                    Console.WriteLine($"Volume label: {drive.VolumeLabel}");
                    Console.WriteLine($"Size: {drive.TotalSize}");
                    Console.WriteLine($"Filesystem: {drive.DriveFormat}");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Drive {drive.Name} is not ready.");
                    Console.WriteLine();
                }
            }
        }
    }
}


