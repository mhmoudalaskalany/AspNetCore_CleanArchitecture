using System.Linq.Expressions;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Template.Api.Controllers.V1.Lookup;
using Template.Application.Services.Lookups.Status;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Status;
using Template.Domain.Entities.Lookup;

namespace Template.Api.Unit.Tests.Controllers.V1.Lookup.Statuses
{
    public class StatusesControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<IStatusService> _statusServiceMock;
        private readonly StatusesController _controller;
        public StatusesControllerUnitTests()
        {
            _statusServiceMock = new Mock<IStatusService>();
            Fixture.Register(() => _statusServiceMock.Object);
            _controller = new StatusesController(_statusServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_Return_Ok()
        {
            // AAA
            //Arrange (set up variables) 
            var testDtos = Fixture.CreateMany<StatusDto>().ToList();
            var result = Result<IEnumerable<StatusDto>>.Success(testDtos);

            _statusServiceMock.Setup(x => x.GetAllAsync(It.IsAny<bool>(), It.IsAny<Expression<Func<Status, bool>>>()))
                .ReturnsAsync(result);

            //Act ( execute the target method )
            var actionResult = await _controller.GetAllAsync();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<StatusDto>>>(objectResult.Value);
            Assert.Equal((int)HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.NotNull(apiResponse.Data);
        }



    }
}