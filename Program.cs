using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// using Microsoft.Extensions.Configuration.Json;
// using Microsoft.Extensions.Configuration.EnvironmentVariables;
using NLog.Extensions.Logging;

namespace tile38test3
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
//            CreateWebHostBuilder(args).Build().Run();


            string environmentName = Environment.GetEnvironmentVariable("EnvironmentName");
            if(environmentName == null || environmentName.Length==0){
                environmentName = "";
            }
            environmentName = environmentName.ToLower();

            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddJsonFile($"appsettings.secret.json", optional: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            try
            {
                //configure NLog
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties =true });
                Console.WriteLine($"ConfigureNLog - NLogConfigurationFilePath: {Program.Configuration["NLogConfigurationFilePath"]}.");

                loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties =true });
                NLog.LogManager.LoadConfiguration(Program.Configuration["NLogConfigurationFilePath"]);


                NLog.LogManager.GetCurrentClassLogger().Info("Tile38test starting up...");
                NLog.LogManager.GetCurrentClassLogger().Info("Branch descriptor:");
                NLog.LogManager.GetCurrentClassLogger().Info("master");

                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(ex);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
