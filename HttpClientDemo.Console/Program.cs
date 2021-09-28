using Microsoft.Extensions.DependencyInjection;
using HttpClientDemo.Console.Configurations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace HttpClientDemo.Console
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // create service collection
            var services = new ServiceCollection();
            AppConfiguration.ConfigureServices(services);

            // create service provider
            var serviceProvider = services.BuildServiceProvider();

            // entry to run app
            await serviceProvider.GetService<App>().Run(args);
        }
    }
}