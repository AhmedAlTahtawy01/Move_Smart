using DataAccessLayer.Repositories;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessLayer.Tests
{
    public class UserDALTests
    {

        private readonly UserDAL _dal;

        public UserDALTests()
        {
            _dal = new UserDAL();
        }

        private User ArrangeUser()
        {
            return new User(
                0,
                "12345678910123",
                $"User_{Guid.NewGuid()}",
                "Tester",
                1
            );
        }

        [Fact]
        public async Task CreateUserAsync_ShouldInsertUser()
        {
            // Arrange
            var user = ArrangeUser();

            // Act
            var newUserId = await _dal.CreateUserAsync(user);

            // Assert
            newUserId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = ArrangeUser();
            var newUserId = await _dal.CreateUserAsync(user);

            // Act
            var retrievedUser = await _dal.GetUserByIdAsync(newUserId);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser.UserId.Should().Be(newUserId);
            retrievedUser.NationalNo.Should().Be(user.NationalNo);
            retrievedUser.Name.Should().Be(user.Name);
            retrievedUser.Role.Should().Be(user.Role);
            retrievedUser.AccessRight.Should().Be(user.AccessRight);
        }

        [Fact]
        public async Task GetUserByNationalNoAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new User(
                0,
                "12345678911123",
                $"User_{Guid.NewGuid()}",
                "Tester",
                1
            );
            var newUserId = await _dal.CreateUserAsync(user);

            // Act
            var retrievedUser = await _dal.GetUserByNationalNoAsync(user.NationalNo);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser.UserId.Should().Be(newUserId);
            retrievedUser.NationalNo.Should().Be(user.NationalNo);
            retrievedUser.Name.Should().Be(user.Name);
            retrievedUser.Role.Should().Be(user.Role);
            retrievedUser.AccessRight.Should().Be(user.AccessRight);

        }

        [Fact]
        public async Task GetAllUserAsync_ShouldReturnUserList()
        {
            // Act
            var users = await _dal.GetAllUsersAsync();

            // Assert
            users.Should().NotBeNull();
            users.Should().BeOfType<List<User>>();
            users.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUserData()
        {
            // Arrange
            var user = ArrangeUser();
            var newUserId = await _dal.CreateUserAsync(user);
            var updatedUser = new User(
                newUserId,
                user.NationalNo,
                $"Updated_{Guid.NewGuid()}",
                "UpdatedTester",
                2
            );

            // Act
            var updateResult = await _dal.UpdateUserAsync(updatedUser);
            var retrievedUser = await _dal.GetUserByIdAsync(newUserId);

            // Assert
            updateResult.Should().BeTrue();
            retrievedUser.Name.Should().Be(updatedUser.Name);
            retrievedUser.Role.Should().Be(updatedUser.Role);
            retrievedUser.AccessRight.Should().Be(updatedUser.AccessRight);

        }

        [Fact]
        public async Task DeleteUserAsync_ShouldRemoveUser()
        {
            // Arrange
            var user = ArrangeUser();
            var newUserId = await _dal.CreateUserAsync(user);

            // Act
            var deleteResult = await _dal.DeleteUserAsync(newUserId);
            var deletedUser = await _dal.GetUserByIdAsync(newUserId);

            // Assert
            deleteResult.Should().BeTrue();
            deletedUser.Should().BeNull();
        }

    }
}