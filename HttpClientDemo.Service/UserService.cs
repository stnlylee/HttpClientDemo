using Microsoft.Extensions.Logging;
using HttpClientDemo.Common.Enums;
using HttpClientDemo.Repository;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<string> GetFirstNameByAge(int age)
        {
            try
            {
                string output = "no result";
                var list = await _userRepository.GetByAge(age);
                if (list?.Any() ?? false)
                {
                    output = string.Join(',', list.Select(x => x.First));
                }

                return output;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                throw;
            }
        }

        public async Task<string> GetFullNameById(int id)
        {
            try
            {
                string output = "no result";
                var user = await _userRepository.GetByKey(id);
                if (user != null)
                {
                    output = $"{user?.First ?? "[No value]"} {user?.Last ?? "[No value]"}";
                }

                return output;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                throw;
            }
        }

        public async Task<string> GroupByAge()
        {
            try
            {
                string output = "no result";
                var list = await _userRepository.GetAll();

                if (list?.Any() ?? false)
                {
                    var stats = list.GroupBy(x => x.Age).Select(g => new
                    {
                        Age = g.Key,
                        Female = g.Count(x => x.Gender == Gender.F),
                        Male = g.Count(x => x.Gender == Gender.M)
                    }).OrderBy(x => x.Age).ToList();

                    if (stats != null && stats.Any())
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var stat in stats)
                        {
                            sb.Append($"Age: {stat.Age} Female: {stat.Female} Male: {stat.Male}\n");
                        }
                        output = sb.ToString();
                    }
                }

                return output;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                throw;
            }
        }
    }
}
