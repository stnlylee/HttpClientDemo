using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Console.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class HttpClientPolicyConstants
    {
        public const string Timeout = "Timeout";
        public const string WaitAndRetry = "WaitAndRetry";
        public const string CircuitBreaker = "CircuitBreaker";
    }
}
