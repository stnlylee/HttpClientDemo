using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using HttpClientDemo.Common.Enums;
using HttpClientDemo.Data.Datasource;
using HttpClientDemo.Data.Dto;
using HttpClientDemo.Repository;
using HttpClientDemo.Repository.Mappings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientDemo.UnitTests.RepositoryTests
{
    public class UserRespositoryTests
    {
        private readonly Mock<IUserDatasource> _mockUserDatasource;
        private readonly Mock<ILogger<UserRepository>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;

        public UserRespositoryTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

            _mockUserDatasource = new Mock<IUserDatasource>();

            _mockLogger = new Mock<ILogger<UserRepository>>();

            _userRepository = new UserRepository(_mockUserDatasource.Object, _mapper, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAll_Should_Get_Data()
        {
            // Arrange
            _mockUserDatasource.Setup(x => x.Users()).ReturnsAsync(GenerateUserDtoList());

            // Act
            var result = await _userRepository.GetAll();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetByAge_Should_Get_Data()
        {
            // Arrange
            _mockUserDatasource.Setup(x => x.Users()).ReturnsAsync(GenerateUserDtoList());

            // Act
            var result = await _userRepository.GetByAge(20);

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByKey_Should_Get_Data()
        {
            // Arrange
            _mockUserDatasource.Setup(x => x.Users()).ReturnsAsync(GenerateUserDtoList());

            // Act
            var result = await _userRepository.GetByKey(1);

            // Assert
            Assert.Equal("John", result.First);
        }

        private List<UserDto> GenerateUserDtoList()
        {
            var list = new List<UserDto>();
            
            list.Add(new UserDto
            {
                Id = 1,
                First = "John",
                Last = "Smith",
                Age = 20,
                Gender = Gender.M
            });

            list.Add(new UserDto
            {
                Id = 2,
                First = "Bill",
                Last = "Bryson",
                Age = 20,
                Gender = Gender.M
            });

            list.Add(new UserDto
            {
                Id = 3,
                First = "Jill",
                Last = "Scott",
                Age = 22,
                Gender = Gender.F
            });

            return list;
        }
    }
}
