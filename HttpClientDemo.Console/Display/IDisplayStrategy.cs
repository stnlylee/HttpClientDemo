using System.Threading.Tasks;

namespace HttpClientDemo.Console.Display
{
    public interface IDisplayStrategy
    {
        string Mode { get; }

        Task Display();
    }
}
