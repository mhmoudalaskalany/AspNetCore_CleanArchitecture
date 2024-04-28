using System.Net;
using AutoFixture;
using Moq;
using Template.Api.Controllers.V1.Lookup;
using Template.Application.Services.Lookups.Action;
using Template.Common.Core;

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
        public async Task GetAllAsync_Return_Ok()
        {
            //Arrange (set up variables) 
            var result = (IFinalResult)Fixture.Build<FinalResult>().With(p => p.Status, HttpStatusCode.OK).Create();
            _actionServiceMock.Setup(x => x.GetAllAsync(It.IsAny<bool>(), null))
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetAllAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }



    }
}