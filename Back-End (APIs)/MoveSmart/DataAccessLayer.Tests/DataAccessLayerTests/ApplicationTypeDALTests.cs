using DataAccessLayer.Repositories;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTests.DataAccessLayerTests
{
    public class ApplicationTypeDALTests
    {

        private readonly ApplicationTypeDAL _dal;

        public ApplicationTypeDALTests() => _dal = new ApplicationTypeDAL();

        [Fact]
        public async Task CreateApplicationTypeAsync_ShouldInsertData()
        {
            // Arrange
            string typeName = "Purchase order";

            // Act
            int newId = await _dal.CreateApplicationTypeAsync(typeName);

            // Assert
            newId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetApplicationTypeByIdAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var typeName = "Release permission";
            var newId = await _dal.CreateApplicationTypeAsync(typeName);

            // Act
            var result = await _dal.GetApplicationTypeByIdAsync(newId);

            // Assert
            result.Should().NotBeNull();
            result.TypeId.Should().Be(newId);
            result.TypeName.Should().Be(typeName);
        }

        [Fact]
        public async Task GetAllApplicationTypesAsync_ShouldReturnData()
        {
            // Act
            var result = await _dal.GetAllApplicationTypesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ApplicationTypeDTO>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateApplicationTypeAsync_ShouldUpdateDate()
        {
            var typeName = "Purchase Order";
            var newId = await _dal.CreateApplicationTypeAsync(typeName);

            var updatedTypeName = "Job Order";

            // Act
            var updateResult = await _dal.UpdateApplicationTypeAsync(new ApplicationTypeDTO(newId, updatedTypeName));
            var updatedApplicationType = await _dal.GetApplicationTypeByIdAsync(newId);

            // Assert
            updateResult.Should().BeTrue();
            updatedApplicationType.TypeName.Should().Be(updatedTypeName);
        }

        [Fact]
        public async Task DeleteApplicationTypeAsync_ShouldRemoveData()
        {
            // Arrange
            var typeName = $"TestType_{Guid.NewGuid()}";
            var newId = await _dal.CreateApplicationTypeAsync(typeName);

            // Act
            var deleteResult = await _dal.DeleteApplicationTypeAsync(new ApplicationTypeDTO(newId, typeName));
            var deletedType = await _dal.GetApplicationTypeByIdAsync(newId);

            // Assert
            deleteResult.Should().BeTrue();
            deletedType.Should().BeNull();
        }

    }
}