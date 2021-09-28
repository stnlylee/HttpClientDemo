using HttpClientDemo.Common.Enums;
using System.Diagnostics.CodeAnalysis;

namespace HttpClientDemo.Data.Dto
{
    [ExcludeFromCodeCoverage]
    public class UserDto : DtoBase
    {
        public string First { get; set; }

        public string Last { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}
