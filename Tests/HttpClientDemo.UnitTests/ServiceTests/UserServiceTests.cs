using Microsoft.Extensions.Logging;
using Moq;
using HttpClientDemo.Common.Enums;
using HttpClientDemo.Domain.Models;
using HttpClientDemo.Repository;
using HttpClientDemo.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace HttpClientDemo.UnitTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();

            _mockLogger = new Mock<ILogger<UserService>>();

            _userService = new UserService(_mockUserRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetFirstNameByAge_Should_Return_NoResult()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetByAge(It.IsAny<int>())).ReturnsAsync(() => { return null; });

            // Act
            var result = await _userService.GetFirstNameByAge(20);

            // Assert
            Assert.Equal("no result", result);
        }

        [Fact]
        public async Task GetFirstNameByAge_Should_Return_Data()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetByAge(It.IsAny<int>()))
                .ReturnsAsync(GenerateUserList().Where(x => x.Age == 20).ToList());

            // Act
            var result = await _userService.GetFirstNameByAge(20);

            // Assert
            Assert.Equal("John,Bill", result);
        }

        [Fact]
        public async Task GetFullNameById_Should_Return_NoResult()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetByKey(It.IsAny<int>())).ReturnsAsync(() => { return null; });

            // Act
            var result = await _userService.GetFullNameById(20);

            // Assert
            Assert.Equal("no result", result);
        }

        [Fact]
        public async Task GetFullNameById_Should_Return_Data()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetByKey(It.IsAny<int>()))
                .ReturnsAsync(GenerateUserList().Where(x => x.Id == 1).FirstOrDefault());

            // Act
            var result = await _userService.GetFullNameById(1);

            // Assert
            Assert.Equal("John Smith", result);
        }

        [Fact]
        public async Task GroupByAge_Should_Return_NoResult()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetAll())
                .ReturnsAsync(() => { return null; });

            // Act
            var result = await _userService.GroupByAge();

            // Assert
            Assert.Equal("no result", result);
        }

        [Fact]
        public async Task GroupByAge_Should_Return_Data()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetAll())
                .ReturnsAsync(GenerateUserList());

            // Act
            var result = await _userService.GroupByAge();

            // Assert
            Assert.Equal("Age: 20 Female: 0 Male: 2\nAge: 22 Female: 1 Male: 0\n", result);
        }

        private List<User> GenerateUserList()
        {
            var list = new List<User>();

            list.Add(new User
            {
                Id = 1,
                First = "John",
                Last = "Smith",
                Age = 20,
                Gender = Gender.M
            });

            list.Add(new User
            {
                Id = 2,
                First = "Bill",
                Last = "Bryson",
                Age = 20,
                Gender = Gender.M
            });

            list.Add(new User
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
