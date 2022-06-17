using System.Runtime.InteropServices;

namespace Words
{
    // delegate for callback fuction
    internal delegate void SignalHandler(ConsoleSignal consoleSignal);

    // all existing console closing signals
    internal enum ConsoleSignal
    {
        CtrlC = 0,
        CtrlBreak = 1,
        Close = 2,
        LogOff = 5,
        Shutdown = 6
    }

    // class that imports SetConsoleCtrlHandler function to handle console closing signals
    internal static class ConsoleHelper
    {
        [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        public static extern bool SetSignalHandler(SignalHandler handler, bool add);
    }
}
