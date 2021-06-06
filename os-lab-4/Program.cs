using System;
using System.Threading;

namespace os_lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Scheduler.Initialize();
            
            var keyThread = new Thread(Scheduler.MonitorKeys);
            keyThread.Start();
            
            while (true)
            {
                Scheduler.Run();
                if (Scheduler.ProcessCount() == 0)
                {
                    Console.WriteLine("Process queue is empty, quitting...");
                    return;
                }
            }
        }
    }
}