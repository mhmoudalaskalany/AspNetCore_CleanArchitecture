using System.Linq.Expressions;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Template.Api.Controllers.V1.Lookup;
using Template.Application.Services.Lookups.Action;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Action;
using Action = Template.Domain.Entities.Lookup.Action;

namespace Template.Api.Unit.Tests.Controllers.V1.Lookup.Actions
{
    public class ActionsControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<IActionService> _actionServiceMock;
        private readonly ActionsController _controller;
        public ActionsControllerUnitTests()
        {
            _actionServiceMock = new Mock<IActionService>();
            Fixture.Register(() => _actionServiceMock.Object);
            _controller = new ActionsController(_actionServiceMock.Object);
        }



        [Fact]
        public async Task GetAll_Return_Ok()
        {
            // AAA
            //Arrange (set up variables) 
            var testDtos = Fixture.CreateMany<ActionDto>().ToList();
            var result = Result<IEnumerable<ActionDto>>.Success(testDtos);

            _actionServiceMock.Setup(x => x.GetAllAsync(It.IsAny<bool>(), It.IsAny<Expression<Func<Action, bool>>>()))
                .ReturnsAsync(result);

            //Act ( execute the target method )
            var actionResult = await _controller.GetAllAsync();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<ActionDto>>>(objectResult.Value);
            Assert.Equal((int)HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.NotNull(apiResponse.Data);
        }


    }
}