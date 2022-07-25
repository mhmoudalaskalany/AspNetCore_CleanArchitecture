using System.Net;
using AutoFixture;
using BackendCore.Api.Controllers.Lookup;
using BackendCore.Common.Core;
using BackendCore.Service.Services.Lookups.Action;
using Moq;

namespace BackendCore.Api.Unit.Tests.Controllers
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
        public async Task GetStatusesAsync_Return_Ok()
        {
            // AAA
            //Arrange (set up variables 

            var result = (IFinalResult)Fixture.Build<Result>().With(p => p.Status, HttpStatusCode.OK).Create();

            _actionServiceMock.Setup(x => x.GetActionsAsync())
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetActionsAsync();


            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }


        
    }
}