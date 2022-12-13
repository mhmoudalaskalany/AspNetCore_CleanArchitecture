using System.Net;
using AutoFixture;
using Moq;
using Template.Api.Controllers.Lookup;
using Template.Application.Services.Lookups.Action;
using Template.Common.Core;

namespace Template.Api.Unit.Tests.Controllers.Actions
{
    public class GetActionsAsyncControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<IActionService> _actionServiceMock;
        private readonly ActionsController _controller;
        public GetActionsAsyncControllerUnitTests()
        {
            _actionServiceMock = new Mock<IActionService>();
            Fixture.Register(() => _actionServiceMock.Object);
            _controller = new ActionsController(_actionServiceMock.Object);
        }

        [Fact]
        public async Task GetActionsAsync_Return_Ok()
        {
            //Arrange (set up variables 
            var result = (IFinalResult)Fixture.Build<FinalResult>().With(p => p.Status, HttpStatusCode.OK).Create();
            _actionServiceMock.Setup(x => x.GetActionsAsync())
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetActionsAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }



    }
}