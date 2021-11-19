using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ModuleHW.DataAccess;

namespace ModuleHW.StartApplication
{
    public class Starter
    {
        public void Run()
        {
            var configFile = "appsettings.json";
            var configFileInfo = new FileInfo(configFile);

            if (configFileInfo.Exists)
            {
                IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(configFile).Build();
                var connectionString = configuration.GetConnectionString("VSMusicDB");

                var serviceProvider = new ServiceCollection()
                    .AddDbContext<ApplicationContext>(optionsDb => optionsDb
                    .UseSqlServer(connectionString, optionsSql => optionsSql
                    .CommandTimeout(30))
                    .UseLazyLoadingProxies())
                    .AddOptions()
                    .BuildServiceProvider();

                using (var db = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>())
                {
                }
            }
            else
            {
                Console.WriteLine($"ERROR! There is no config file \"{configFile}\" provided!");
                Environment.Exit(0);
            }
        }
    }
}
