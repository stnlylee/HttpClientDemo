using HttpClientDemo.Service;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace HttpClientDemo.Console.Display
{
    [ExcludeFromCodeCoverage]
    public class DisplayGetFirstNameByAge : IDisplayStrategy
    {
        private readonly IUserService _userService;

        public string Mode => "2";

        public DisplayGetFirstNameByAge(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Display()
        {
            System.Console.WriteLine("running DisplayGetFirstNameByAge...");
            System.Console.WriteLine("Please enter age:");
            var input = System.Console.ReadLine();
            int age;
            if (int.TryParse(input, out age) && age > 0)
            {
                try
                {
                    System.Console.WriteLine(await _userService.GetFirstNameByAge(age));
                }
                catch (Exception)
                {
                    System.Console.WriteLine("something went wrong");
                }
            }
            else
            {
                System.Console.WriteLine("age is not valid");
            }
        }
    }
}
