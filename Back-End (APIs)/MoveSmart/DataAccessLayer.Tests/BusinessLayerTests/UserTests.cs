using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BusinessLayer.Services;
using DataAccessLayer.Repositories;
using FluentAssertions;
using Xunit;

namespace ProjectTests.BusinessLayerTests
{
    public class UserTests
    {
        private async Task<User> CreateTestUserAsync()
        {
            var testUserDTO = new UserDTO(0, "12345678901238", "Test@123", "Test User", enUserRole.GeneralManager, 1);
            var user = new User(testUserDTO);
            bool created = await user.SaveAsync();
            created.Should().BeTrue();
            return user;
        }

        [Fact]
        public async Task SaveAsync_Should_CreateUser_When_UserIsNew()
        {
            // Arrange
            var user = await CreateTestUserAsync();

            // Assert
            user.UserId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task SaveAsync_Should_UpdateUser_When_UserAlreadyExists()
        {
            // Arrange
            var user = await CreateTestUserAsync();
            user.Name = "Updated Name";

            // Act
            bool result = await user.SaveAsync();

            // Assert
            result.Should().BeTrue();
            var updatedUser = await user.GetUserByIdAsync(user.UserId);
            updatedUser.Name.Should().Be("Updated Name");
        }

        [Fact]
        public async Task GetAllUsersAsync_Should_Return_ListOfUsers()
        {
            // Arrange
            var user = new User(new UserDTO(0, "", "", "", enUserRole.GeneralManager, 1));

            // Act
            List<UserDTO> users = await user.GetAllUsersAsync();

            // Assert
            users.Should().NotBeNull();
            users.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetUserByIdAsync_Should_Return_CorrectUser()
        {
            // Arrange
            var user = await CreateTestUserAsync();
            int userId = user.UserId;

            // Act
            var retrievedUser = await user.GetUserByIdAsync(userId);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser.UserId.Should().Be(userId);
        }

        [Fact]
        public async Task GetUserByNationalNoAsync_Should_Return_CorrectUser()
        {
            // Arrange
            var user = await CreateTestUserAsync();

            // Act
            var retrievedUser = await user.GetUserByNationalNoAsync(user.NationalNo);

            // Assert
            retrievedUser.Should().NotBeNull();
            retrievedUser.NationalNo.Should().Be(user.NationalNo);
        }

        [Fact]
        public async Task DeleteUserAsync_Should_RemoveUserSuccessfully()
        {
            // Arrange
            var user = await CreateTestUserAsync();
            int userId = user.UserId;

            // Act
            bool deleted = await user.DeleteUserAsync(userId);
            var retrievedUser = await user.GetUserByIdAsync(userId);

            // Assert
            deleted.Should().BeTrue();
            retrievedUser.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_Should_ReturnUser_When_CredentialsAreCorrect()
        {
            // Arrange
            var user = await CreateTestUserAsync();

            // Act
            var loggedInUser = await user.LoginAsync(user.NationalNo, "Test@123");

            // Assert
            loggedInUser.Should().NotBeNull();
            loggedInUser.UserId.Should().Be(user.UserId);
        }

        [Fact]
        public async Task LoginAsync_Should_ThrowException_When_PasswordIsIncorrect()
        {
            // Arrange
            var user = await CreateTestUserAsync();

            // Act
            Func<Task> act = async () => await user.LoginAsync(user.NationalNo, "WrongPassword");

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Invalid password.");
        }

        [Fact]
        public async Task ChangePasswordAsync_Should_UpdatePasswordSuccessfully()
        {
            // Arrange
            var user = await CreateTestUserAsync();
            string newPassword = "NewPass@456";

            // Act
            bool result = await user.ChangePasswordAsync(newPassword);
            var loggedInUser = await user.LoginAsync(user.NationalNo, newPassword);

            // Assert
            result.Should().BeTrue();
            loggedInUser.Should().NotBeNull();
            loggedInUser.UserId.Should().Be(user.UserId);
        }
    }
}
