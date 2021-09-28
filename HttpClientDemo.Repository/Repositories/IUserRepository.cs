using HttpClientDemo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientDemo.Repository
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<List<User>> GetByAge(int age);
    }
}
