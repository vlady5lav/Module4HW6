using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ModuleHW.DataAccess
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var configFile = "appsettings.json";
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile(configFile).Build();

            var connectionString = configuration.GetConnectionString("VSMusicDB");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString, s => s.CommandTimeout(30));

            return new ApplicationContext(dbContextOptionsBuilder.Options);
        }
    }
}
