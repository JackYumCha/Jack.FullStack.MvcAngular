using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MvcAngular.Generator;

namespace Jack.FullStack.MvcAngular.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (AngularGenerator.ShouldRunMvc(args))
            {
                var isDevelopment = (Debugger.IsAttached || args.Contains("--development"));
                var builder = CreateWebHostBuilder(
                    args.Where(arg => arg != "--console" && arg != "--development").ToArray(),
                    isDevelopment ? "https://*:9188;http://*:9186" : "https://*:443;http://*:80"); // 

                // to run this console app as a windows service:

                /*
                 * 
                 NSSM is the tool to set up windows service for any console app.

                http://nssm.cc/usage

                In Ubuntu Linux, a service could be any console app. In Windows, a wrapper is required.

                1. Download and extrace the NSSM to any folder.
                2. Run the “nssm install <service name>” to create you service.
                3. Make sure in the “Log On” tab, put your administrator username and password. Otherwise, the console app may not run properly. (In my case, the web service is not working without logon.).
                 
                 */

                var host = builder.Build();
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, string urls) => 
            WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()    
            .UseUrls(urls);
    }
}
