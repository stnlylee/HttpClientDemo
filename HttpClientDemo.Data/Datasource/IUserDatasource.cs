using HttpClientDemo.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpClientDemo.Data.Datasource
{
    public interface IUserDatasource
    {
        Task<List<UserDto>> Users();
    }
}
