using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyBlog.Location.Api
{
    public static class Program
    {
        public static readonly string AppName;

        static Program()
        {
            var ns = typeof(Program).Namespace;
            AppName = ns.Substring(ns.LastIndexOf('.', ns.LastIndexOf('.') - 1) + 1);
        }

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
