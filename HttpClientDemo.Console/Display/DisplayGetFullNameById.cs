using HttpClientDemo.Service;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace HttpClientDemo.Console.Display
{
    [ExcludeFromCodeCoverage]
    public class DisplayGetFullNameById : IDisplayStrategy
    {
        private readonly IUserService _userService;

        public string Mode => "1";

        public DisplayGetFullNameById(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Display()
        {
            System.Console.WriteLine("running DisplayGetFullNameById...");
            System.Console.WriteLine("Please enter id:");
            var input = System.Console.ReadLine();
            int id;
            if (int.TryParse(input, out id) && id > 0)
            {
                try
                {
                    System.Console.WriteLine(await _userService.GetFullNameById(id));
                }
                catch (Exception)
                {
                    System.Console.WriteLine("something went wrong");
                }
            }
            else
            {
                System.Console.WriteLine("id is not valid");
            }
        }
    }
}
