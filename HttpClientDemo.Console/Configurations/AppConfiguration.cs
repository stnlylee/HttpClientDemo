using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly.Registry;
using HttpClientDemo.Common.Cache;
using HttpClientDemo.Console.Display;
using HttpClientDemo.Console.Settings;
using HttpClientDemo.Data.Datasource;
using HttpClientDemo.Repository;
using HttpClientDemo.Repository.Mappings;
using HttpClientDemo.Service;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace HttpClientDemo.Console.Configurations
{
    [ExcludeFromCodeCoverage]
    public class AppConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // configure logging
            services.AddLogging(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Error)
                       .AddFilter("System", LogLevel.Error)
                       .AddConsole();
            });

            // build config
            var env = Environment.GetEnvironmentVariable("Env");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "\\Settings")
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: false, true)
                .AddEnvironmentVariables()
                .Build();
            services.Configure<AppSettings>(configuration.GetSection("App"));

            // distributed cache
            services.AddDistributedMemoryCache();
            services.AddSingleton<IDistributedCacheProvider, DistributedCacheProvider>();

            // add automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // polly
            services.AddPolicyRegistry(new PolicyRegistry {
                {HttpClientPolicyConstants.Timeout, HttpClientPolicyFactory.TimeoutPolicy()},
                {HttpClientPolicyConstants.WaitAndRetry, HttpClientPolicyFactory.WaitAndRetry()},
                {HttpClientPolicyConstants.CircuitBreaker, HttpClientPolicyFactory.CircuitBreakerPolicy()},
            });

            // add http client
            services.AddHttpClient("UserClient", c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection("App:UserApiBaseUrl").Value);
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandlerFromRegistry(HttpClientPolicyConstants.Timeout)
            .AddPolicyHandlerFromRegistry(HttpClientPolicyConstants.WaitAndRetry)
            .AddPolicyHandlerFromRegistry(HttpClientPolicyConstants.CircuitBreaker);

            // add services
            services.AddTransient<IUserService, UserService>();

            // add display strategy
            services.AddTransient<IDisplayStrategy, DisplayGetFirstNameByAge>();
            services.AddTransient<IDisplayStrategy, DisplayGetFullNameById>();
            services.AddTransient<IDisplayStrategy, DisplayGroupByAge>();

            // add data
            services.AddTransient<IUserDatasource, UserApiDatasource>();

            // add repository
            services.AddTransient<IUserRepository, UserRepository>();

            // add app
            services.AddTransient<App>();
        }
    }
}
