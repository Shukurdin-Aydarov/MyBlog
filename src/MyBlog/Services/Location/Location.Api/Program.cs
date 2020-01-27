using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Location.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(lb =>
                {
                    lb.ClearProviders();
                    lb.AddConsole();
                })
                .ConfigureWebHostDefaults(wb => wb.UseStartup<Startup>());
    }
}
