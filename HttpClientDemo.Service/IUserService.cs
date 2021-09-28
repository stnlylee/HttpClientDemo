using System.Threading.Tasks;

namespace HttpClientDemo.Service
{
    public interface IUserService
    {
        Task<string> GetFullNameById(int id);

        Task<string> GetFirstNameByAge(int age);

        Task<string> GroupByAge();
    }
}
