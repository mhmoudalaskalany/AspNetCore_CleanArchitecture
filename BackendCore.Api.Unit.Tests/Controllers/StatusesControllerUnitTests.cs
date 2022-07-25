using System.Net;
using AutoFixture;
using BackendCore.Api.Controllers.Lookup;
using BackendCore.Common.Core;
using BackendCore.Service.Services.Lookups.Status;
using Moq;

namespace BackendCore.Api.Unit.Tests.Controllers
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