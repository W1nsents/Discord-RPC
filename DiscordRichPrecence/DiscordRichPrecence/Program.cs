using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Runtime.InteropServices;
using System.Timers;

namespace DiscordRichPresence
{
    internal class Program
    {

        #region DLL
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_Min = 2;
        const int SW_Max = 3;
        const int SW_Norm = 4;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        #endregion

        public static DiscordRpcClient client;
        public static void Main(string[] args)
        {
            // CONSOLE HIDE
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            client = new DiscordRpcClient("application ID");
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
            client.Initialize();

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Elapsed += (sender, e) => 
            {
                client.SetPresence(new RichPresence()
                {
                    Details = "TEXT",
                    State = "TEXT",
                    Assets = new Assets()
                    {
                        LargeImageKey = "gif, png url",
                        LargeImageText = "TEXT",
                    },
                    Buttons = new Button[]
                    {
                        new Button() { Label = "TEXT", Url= "site url" },
                        new Button() { Label = "TEXT", Url = "site url" }
                    }
                });
            };
            Console.ReadLine();
        }
    }
}
