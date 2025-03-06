using DataAccessLayer.Repositories;
using FluentAssertions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTests.DataAccessLayerTests
{
    public class ApplicationDALTests
    {
        private readonly ApplicationDAL _applicationDAL;

        public ApplicationDALTests()
        {
            _applicationDAL = new ApplicationDAL();
        }

        //private Application ArrangeApplication()
        //{
        //    return 
        //}

        [Fact]
        public async Task CreateApplicationAsync_ShouldInsertData()
        {
            // Arrange
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Canceled,
                4,
                "Test application",
                1
            );

            // Act
            int newId = await _applicationDAL.CreateApplicationAsync(application);

            // Assert
            newId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetAllApplicationsAsync_ShouldReturnApplicationList()
        {
            // Act
            var applications = await _applicationDAL.GetAllApplicationsAsync();

            // Assert
            applications.Should().NotBeNull();
            applications.Should().BeOfType<List<Application>>();
            applications.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetApplicationByIdAsync_ShouldReturnCorrectApplication()
        {
            // Arrange
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Pending,
                4,
                "Test application",
                1
            );

            var newApplicationId = await _applicationDAL.CreateApplicationAsync(application);

            // Act
            var retrievedApplication = await _applicationDAL.GetApplicationByIdAsync(newApplicationId);

            // Assert
            retrievedApplication.Should().NotBeNull();
            retrievedApplication.ApplicationId.Should().Be(newApplicationId);
            retrievedApplication.CreationDate.Date.Should().Be(application.CreationDate.Date);
            retrievedApplication.Status.Should().Be(application.Status);
            retrievedApplication.ApplicationType.Should().Be(application.ApplicationType);
            retrievedApplication.ApplicationDescription.Should().Be(application.ApplicationDescription);
            retrievedApplication.UserId.Should().Be(application.UserId);
        }

        [Fact]
        public async Task GetApplicationsByApplicationTypeAsync_ShouldReturnApplications()
        {
            // Arrange
            var applicationType = 3;
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Pending,
                applicationType,
                "Test Application",
                1
            );
            await _applicationDAL.CreateApplicationAsync(application);

            // Act
            var applications = await _applicationDAL.GetApplicationsByApplicationTypeAsync(applicationType);

            // Assert
            applications.Should().NotBeNull();
            applications.Should().BeOfType<List<Application>>();
            applications.Should().Contain(a => a.ApplicationType == applicationType);
        }

        [Fact]
        public async Task GetApplicationsByCreatedByUserAsync_ShouldReturnApplications()
        {
            // Arrange
            var createdByUser = 1;
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Pending,
                3,
                "Test Application",
                createdByUser
            );
            await _applicationDAL.CreateApplicationAsync(application);

            // Act
            var applications = await _applicationDAL.GetApplicationsByUserIdAsync(createdByUser);

            // Assert
            applications.Should().NotBeNull();
            applications.Should().BeOfType<List<Application>>();
            applications.Should().Contain(a => a.UserId == createdByUser);
        }

        [Fact]
        public async Task GetApplicationsByStatus_ShouldReturnApplications()
        {
            // Arrange
            var status = enStatus.Pending;
            var application = new Application(
                0,
                DateTime.UtcNow,
                status,
                4,
                "Test Application",
                1
            );
            await _applicationDAL.CreateApplicationAsync(application);

            // Act
            var applications = await _applicationDAL.GetApplicationsByStatus(status);

            // Assert
            applications.Should().NotBeNull();
            applications.Should().BeOfType<List<Application>>();
            applications.Should().Contain(a => a.Status == status);
        }

        [Fact]
        public async Task GetAllApplicationsCountAsync_ShouldReturnCorrectCount()
        {
            // Act
            var count = await _applicationDAL.CountAllApplicationsAsync();

            // Assert
            count.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task GetAllApplicationCountByStatusAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            var status = enStatus.Pending;

            // Act
            var count = await _applicationDAL.CountApplicationsByStatusAsync(status);

            // Assert
            count.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task GetAllApplicationCountByApplicationTypeAsync_ShouldReturnCorrectCount()
        {
            // Arrange
            var applicationType = 1;

            // Act
            var count = await _applicationDAL.CountApplicationsByTypeAsync(applicationType);

            // Assert
            count.Should().BeGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async Task UpdateStatusAsync_ShouldUpdateApplicationStatus()
        {
            // Arrange
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Pending,
                1,
                "Test Application",
                1
            );
            var newApplicationId = await _applicationDAL.CreateApplicationAsync(application);

            // Act
            var updateResult = await _applicationDAL.UpdateStatusAsync(newApplicationId, enStatus.Confirmed);
            var updatedApplication = await _applicationDAL.GetApplicationByIdAsync(newApplicationId);

            // Assert
            updateResult.Should().BeTrue();
            updatedApplication.Status.Should().Be(enStatus.Confirmed);
        }

        [Fact]
        public async Task UpdateApplicationAsync_ShouldUpdateApplicationData()
        {
            // Arrange
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Pending,
                4,
                "Initial Description",
                1
            );
            var newApplicationId = await _applicationDAL.CreateApplicationAsync(application);

            var updatedApplication = new Application(
                newApplicationId,
                DateTime.UtcNow,
                enStatus.Rejected,
                3,
                "Updated Description",
                1
            );

            // Act
            var updateResult = await _applicationDAL.UpdateApplicationAsync(updatedApplication);
            var retrievedApplication = await _applicationDAL.GetApplicationByIdAsync(newApplicationId);

            // Assert
            updateResult.Should().BeTrue();
            retrievedApplication.Status.Should().Be(updatedApplication.Status);
            retrievedApplication.ApplicationType.Should().Be(updatedApplication.ApplicationType);
            retrievedApplication.ApplicationDescription.Should().Be(updatedApplication.ApplicationDescription);
        }

        [Fact]
        public async Task DeleteApplicationAsync_ShouldRemoveApplication()
        {
            // Arrange
            var application = new Application(
                0,
                DateTime.UtcNow,
                enStatus.Pending,
                4,
                "Application to Delete",
                1
            );
            var newApplicationId = await _applicationDAL.CreateApplicationAsync(application);

            // Act
            var deleteResult = await _applicationDAL.DeleteApplicationAsync(newApplicationId);
            var deletedApplication = await _applicationDAL.GetApplicationByIdAsync(newApplicationId);

            // Assert
            deleteResult.Should().BeTrue();
            deletedApplication.Should().BeNull();
        }

    }
}
