using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;
using BusinessLayer.Services;
using FluentAssertions;

namespace ProjectTests.BusinessLayerTests
{
    public class ApplicationTypeTests
    {
        private readonly ApplicationType _appType;

        public ApplicationTypeTests()
        {
            _appType = new ApplicationType(
                new ApplicationTypeDTO(
                    1234,
                    "Purchase Order"
            ));
        }

        [Fact]
        public async Task CreateApplicationTypeAsync_ShouldInsertData()
        {
            // Arrange: Declare and initialize test variables
            string typeName = "Buisness Test Type";

            // Act: Call the function that will be tested
            bool flag = await _appType.Save();

            // Assert: test the function result
            flag.Should().BeTrue();

        }

    }
}
