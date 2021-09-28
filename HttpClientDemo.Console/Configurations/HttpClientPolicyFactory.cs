using Polly;
using Polly.Extensions.Http;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace HttpClientDemo.Console.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientPolicyFactory
    {
        public static IAsyncPolicy<HttpResponseMessage> TimeoutPolicy()
        {
            return Policy
                .TimeoutAsync<HttpResponseMessage>(10);
        }

        public static IAsyncPolicy<HttpResponseMessage> WaitAndRetry()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(5)
                    }
                );
        }

        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                   .HandleTransientHttpError()
                   .AdvancedCircuitBreakerAsync(
                                                failureThreshold: 0.5,
                                                samplingDuration: TimeSpan.FromSeconds(10),
                                                minimumThroughput: 6,
                                                durationOfBreak: TimeSpan.FromSeconds(10));
        }

        public static class HttpClientPolicyName
        {
            public const string Timeout = nameof(System.Threading.Timeout);
        }
    }
}
