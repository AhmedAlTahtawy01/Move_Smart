using DataAccessLayer.Repositories;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace DataAccessLayer.Tests
{
    public class ApplicationTypeDALTests
    {

        private readonly ApplicationTypeDAL _applicationTypeDAL;

        public ApplicationTypeDALTests()
        {
            _applicationTypeDAL = new ApplicationTypeDAL();
        }

        [Fact]
        public async Task CreateApplicationTypeAsync_ShouldInsertData()
        {
            // Arrange
            string typeName = "TestType";

            // Act
            int newId = await _applicationTypeDAL.CreateApplicationTypeAsync(typeName);

            // Assert
            newId.Should().BeGreaterThan(0);

            // Clean up (Delete the test data)
            await _applicationTypeDAL.DeleteApplicationTypeAsync(newId);
        }

        [Fact]
        public async Task GetApplicationTypeByIdAsync_ShouldReturnCorrectData()
        {
            // Arrange
            var typeName = "Release permission";
            var newId = await _applicationTypeDAL.CreateApplicationTypeAsync(typeName);

            // Act
            var result = await _applicationTypeDAL.GetApplicationTypeByIdAsync(newId);

            // Assert
            result.Should().NotBeNull();
            result.TypeId.Should().Be(newId);
            result.TypeName.Should().Be(typeName);
        }

        [Fact]
        public async Task GetAllApplicationTypesAsync_ShouldReturnData()
        {
            // Act
            var result = await _applicationTypeDAL.GetAllApplicationTypesAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ApplicationType>>();
            result.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task UpdateApplicationTypeAsync_ShouldUpdateDate()
        {
            var typeName = "Release Order";
            var newId = await _applicationTypeDAL.CreateApplicationTypeAsync(typeName);

            var updatedTypeName = "Purchase Order";

            // Act
            var updateResult = await _applicationTypeDAL.UpdateApplicationTypeAsync(new ApplicationType(newId, updatedTypeName));
            var updatedApplicationType = await _applicationTypeDAL.GetApplicationTypeByIdAsync(newId);

            // Assert
            updateResult.Should().BeTrue();
            updatedApplicationType.TypeName.Should().Be(updatedTypeName);
        }

        [Fact]
        public async Task DeleteApplicationTypeAsync_ShouldRemoveData()
        {
            // Arrange
            var typeName = $"TestType_{Guid.NewGuid()}";
            var newId = await _applicationTypeDAL.CreateApplicationTypeAsync(typeName);

            // Act
            var deleteResult = await _applicationTypeDAL.DeleteApplicationTypeAsync(newId);
            var deletedType = await _applicationTypeDAL.GetApplicationTypeByIdAsync(newId);

            // Assert
            deleteResult.Should().BeTrue();
            deletedType.Should().BeNull();
        }
    
    }
}