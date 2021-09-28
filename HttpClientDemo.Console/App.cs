using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HttpClientDemo.Console.Display;
using HttpClientDemo.Console.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Console
{
    [ExcludeFromCodeCoverage]
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly AppSettings _appSettings;
        private readonly IEnumerable<IDisplayStrategy> _displays;
        private List<string> ValidDisplayModes = new List<string> { "1", "2", "3" };
        public App(IOptions<AppSettings> appSettings, ILogger<App> logger,
            IEnumerable<IDisplayStrategy> displays)
        {
            _logger = logger;
            _appSettings = appSettings?.Value;
            _displays = displays;
        }

        public async Task Run(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine("Please choose display mode (1 - 3):");
                var selectedMode = System.Console.ReadLine();
                if (ValidDisplayModes.Contains(selectedMode)) 
                {
                    var display = _displays.SingleOrDefault(x => x.Mode == selectedMode);
                    await display.Display();
                }
                else
                {
                    System.Console.WriteLine($"wrong selection");
                }
            }

            await Task.CompletedTask;
        }
    }
}
