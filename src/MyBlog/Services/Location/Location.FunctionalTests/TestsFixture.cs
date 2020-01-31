using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace MyBlog.Location.FunctionalTests
{
    public class TestsFixture
    {
        public static TestServer CreatrServer()
        {
            var path = Assembly.GetAssembly(typeof(TestsFixture))
                               .Location;

            var hb = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", true)
                      .AddEnvironmentVariables();
                })
                .UseStartup<TestsStartup>();

            return new TestServer(hb);
        }
    }
}
