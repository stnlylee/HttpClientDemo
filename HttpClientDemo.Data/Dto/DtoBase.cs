using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Data.Dto
{
    [ExcludeFromCodeCoverage]
    public abstract class DtoBase
    {
        public int Id { get; set; }
    }
}
