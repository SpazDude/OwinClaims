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
            
            var options = new StartOptions();
            options.Urls.Add("https://*:44300");

            using (WebApp.Start<Startup>(options))
            {
                var urls = string.Join(", ", options.Urls);
                Console.WriteLine("\n\nServer listening at {0}. Press enter to stop", urls);
                Console.ReadLine();
            }
        }
    }
}
