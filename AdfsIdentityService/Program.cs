using System;
using Microsoft.Owin.Hosting;
using Serilog;

namespace IdentityService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IdentityService w/ WS-Federation SelfHost";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            const string url = "https://localhost:44333";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("\n\nServer listening at {0}. Press enter to stop", url);
                Console.ReadLine();
            }
        }
    }
}
