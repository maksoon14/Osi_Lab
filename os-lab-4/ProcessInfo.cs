using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace os_lab_4
{
    public class ProcessInfo
    {
        private const string ModulePath = "C:\\Windows\\system32\\mspaint.exe";
        
        private readonly Process _process;
        public int Quantum;
        public int Priority;
        public string Name;

        public ProcessInfo()
        {
            _process = new Process{StartInfo = new ProcessStartInfo(ModulePath)};
            _process.Start();
        }

        public bool Exited() => _process.HasExited;

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
        
        [DllImport("kernel32", CharSet = CharSet.Auto,SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        
        public void Suspend()
        {
            try
            {
                foreach (ProcessThread thread in _process.Threads)
                {
                    var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id);
                    if (pOpenThread == IntPtr.Zero)
                        break;
                    SuspendThread(pOpenThread);
                    CloseHandle(pOpenThread);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Resume()
        {
            try
            {
                foreach (ProcessThread thread in _process.Threads)
                {
                    var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint) thread.Id);
                    if (pOpenThread == IntPtr.Zero)
                        break;
                    ResumeThread(pOpenThread);
                    CloseHandle(pOpenThread);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}