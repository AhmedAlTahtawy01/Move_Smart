using DataAccessLayer.Repositories;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTests.DataAccessLayerTests
{
    public class UserDALTests
    {

        private readonly UserDAL _dal;

        public UserDALTests() => _dal = new UserDAL();

        [Fact]
        public async Task CreateUserAsync_ShouldInsertUser()
        {
            // Arrange
            var user = new UserDTO(
                0,
                "30310012614555",
                "Ahmed.Super1310",
                "Ahmed Hamdy",
                enUserRole.SuperUser,
                -1
            );

            // Act
            var newUserId = await _dal.CreateUserAsync(user);

            // Assert
            newUserId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new UserDTO(
                0,
                "30310012614556",
                "Abdo.Super1310",
                "Abdo Mohamed",
                enUserRole.SuperUser,
                -1
            );
            var newUserId = await _dal.CreateUserAsync(user);

            // Act
            var retrievedUser = await _dal.GetUserByIdAsync(newUserId);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser.UserId.Should().Be(newUserId);
            retrievedUser.NationalNo.Should().Be(user.NationalNo);
            retrievedUser.Password.Should().Be(user.Password);
            retrievedUser.Name.Should().Be(user.Name);
            retrievedUser.Role.Should().Be(user.Role);
            retrievedUser.AccessRight.Should().Be(user.AccessRight);
        }

        [Fact]
        public async Task GetUserByNationalNoAsync_ShouldReturnCorrectUser()
        {
            // Arrange
            var user = new UserDTO(
                0,
                "30310012614557",
                "Kamal.Super1310",
                "Kamal Emad",
                enUserRole.SuperUser,
                -1
            );
            var newUserId = await _dal.CreateUserAsync(user);

            // Act
            var retrievedUser = await _dal.GetUserByNationalNoAsync(user.NationalNo);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser.UserId.Should().Be(newUserId);
            retrievedUser.NationalNo.Should().Be(user.NationalNo);
            retrievedUser.Password.Should().Be(user.Password);
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
            users.Should().BeOfType<List<UserDTO>>();
            users.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUserData()
        {
            
            var userId = 3;
            var updatedUser = new UserDTO(
                3,
                "40310012614556",
                "AhmedAdel.Super1310",
                "Ahmed Adel",
                enUserRole.SuperUser,
                -1
            );

            // Act
            var updateResult = await _dal.UpdateUserAsync(updatedUser);
            var retrievedUser = await _dal.GetUserByIdAsync(userId);

            // Assert
            updateResult.Should().BeTrue();
            retrievedUser.NationalNo.Should().Be(updatedUser.NationalNo);
            retrievedUser.Password.Should().Be(updatedUser.Password);
            retrievedUser.Name.Should().Be(updatedUser.Name);
            retrievedUser.Role.Should().Be(updatedUser.Role);
            retrievedUser.AccessRight.Should().Be(updatedUser.AccessRight);

        }

        [Fact]
        public async Task DeleteUserAsync_ShouldRemoveUser()
        {
            // Arrange
            var user = new UserDTO(
                0,
                "40310012614510",
                "Adel.Super1310",
                "Ahmed Adel",
                enUserRole.SuperUser,
                -1
            );
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