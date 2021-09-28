using AutoMapper;
using Microsoft.Extensions.Logging;
using HttpClientDemo.Data.Datasource;
using HttpClientDemo.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientDemo.Repository
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        private readonly IUserDatasource _userDatasource;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(IUserDatasource userDatasource, IMapper mapper,
            ILogger<UserRepository> logger)
        {
            _userDatasource = userDatasource;
            _mapper = mapper;
            _logger = logger;
        }

        public async override Task<List<User>> GetAll()
        {
            try
            {
                return _mapper.Map<List<User>>(await _userDatasource.Users());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                throw;
            }
        }

        public async Task<List<User>> GetByAge(int age)
        {
            try
            {
                var list = await _userDatasource.Users();
                return _mapper.Map<List<User>>(list.Where(x => x.Age == age));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                throw;
            }
        }

        public async override Task<User> GetByKey(int key)
        {
            try
            {
                var list = await _userDatasource.Users();
                return _mapper.Map<User>(list.FirstOrDefault(x => x.Id == key));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "something went wrong");
                throw;
            }
        }
    }
}
