using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace TestingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        var connection = "Endpoint=https://me-app-config.azconfig.io;Id=vhmK-l4-s0:YxjBN92AKGXtweCqIkEf;Secret=j6ZTMoC7ssnUZqMg7udSRFuhX9Ws1IBksL09NVLGIao=";
                        config.AddAzureAppConfiguration(options =>
                        options.Connect(connection)
                        .UseFeatureFlags(op => op.CacheExpirationInterval = TimeSpan.FromSeconds(5)));
                    });
    //});
    }
}
