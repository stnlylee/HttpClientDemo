using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using HttpClientDemo.Data.Dto;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using HttpClientDemo.Data.Extension;
using HttpClientDemo.Common.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Data.Datasource
{
    [ExcludeFromCodeCoverage]
    public class UserApiDatasource : IUserDatasource
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserApiDatasource> _logger;
        private readonly IDistributedCacheProvider _distributedCacheProvider;
        private const string AllUserL1CacheKey = "all-users-l1-cache";

        public UserApiDatasource(IHttpClientFactory httpClientFactory,
            ILogger<UserApiDatasource> logger,
            IDistributedCacheProvider distributedCacheProvider)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _distributedCacheProvider = distributedCacheProvider;
        }

        public async Task<List<UserDto>> Users()
        {
            try
            {
                return await TryGetAllFromCache();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error occurred when getting data from service");
                throw;
            }
        }

        private async Task<List<UserDto>> TryGetAllFromCache()
        {
            List<UserDto> list = null;

            list = await _distributedCacheProvider.GetFromCache<List<UserDto>>(AllUserL1CacheKey);

            if (list == null)
            {
                var client = _httpClientFactory.CreateClient("UserClient");
                var response = await client.GetAsync("sampletest");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                var serializationSettings = new JsonSerializerSettings
                {
                    Error = HandleDeserializationError
                };
                list = JsonConvert.DeserializeObject<List<UserDto>>(jsonString, serializationSettings);
                list = list.DistinctBy(x => x.Id).ToList();

                var options = new DistributedCacheEntryOptions();
                options.SetSlidingExpiration(new TimeSpan(0, 0, 30));
                await _distributedCacheProvider.SetCache<List<UserDto>>(AllUserL1CacheKey, list, options);
            }

            return list;
        }

        private void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            errorArgs.ErrorContext.Handled = true;
            var currentObj = errorArgs.CurrentObject as UserDto;

            if (currentObj == null) return;
            currentObj.Gender = Common.Enums.Gender.Unknown;
        }
    }
}
