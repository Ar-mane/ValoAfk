using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace ValoAfk.controllers
{
    public static class Windows
    {
        private const string ValorantProcessName = "VALORANT-Win64-Shipping";
        private const string ValorantGameProcessName = "VALORANT";

        public static bool FocusOnValorant()
        {
            // needs to be fixed
            var processes = Process.GetProcessesByName(ValorantProcessName);
            var processesGame = Process.GetProcessesByName(ValorantGameProcessName);
            return (from process in processes.Concat(processesGame)
                where process.MainWindowHandle.ToInt32() != 0
                select Process.GetProcessById(process.Id)
                into targetProcess
                select targetProcess.MainWindowHandle).Any(FocusOn);
        }

        private static bool FocusOn(IntPtr ptr) => SetForegroundWindow(ptr);
        public static void PressButton(byte character) => keybd_event(character, 0, 1, 0);

        [DllImport("User32.DLL")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
    }
}