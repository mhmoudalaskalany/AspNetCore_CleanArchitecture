using System.Net;
using Application.Services.Lookups.Status;
using AutoFixture;
using Common.Core;
using Moq;
using Template.Api.Controllers.Lookup;

namespace Template.Api.Unit.Tests.Controllers.Statuses
{
    public class GetStatusesAsyncControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<IStatusService> _statusServiceMock;
        private readonly StatusesController _controller;
        public GetStatusesAsyncControllerUnitTests()
        {
            _statusServiceMock = new Mock<IStatusService>();
            Fixture.Register(() => _statusServiceMock.Object);
            _controller = new StatusesController(_statusServiceMock.Object);
        }

        [Fact]
        public async Task GetActionsAsync_Return_Ok()
        {
            //Arrange (set up variables 
            var result = (IFinalResult)Fixture.Build<Result>().With(p => p.Status, HttpStatusCode.OK).Create();
            _statusServiceMock.Setup(x => x.GetStatusesAsync())
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetStatusesAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }



    }
}