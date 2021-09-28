using HttpClientDemo.Service;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace HttpClientDemo.Console.Display
{
    [ExcludeFromCodeCoverage]
    public class DisplayGroupByAge : IDisplayStrategy
    {
        private readonly IUserService _userService;

        public string Mode => "3";

        public DisplayGroupByAge(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Display()
        {
            System.Console.WriteLine("running DisplayGroupByAge...");
            
            try
            {
                System.Console.WriteLine(await _userService.GroupByAge());
            }
            catch (Exception)
            {
                System.Console.WriteLine("something went wrong");
            }
        }
    }
}
