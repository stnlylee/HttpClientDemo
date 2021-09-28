using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public abstract class ModelBase
    {
        public int Id { get; set; }
    }
}
