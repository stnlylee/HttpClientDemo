using HttpClientDemo.Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Domain.Models
{
    [ExcludeFromCodeCoverage]
    public class User : ModelBase
    {
        public string First { get; set; }

        public string Last { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
