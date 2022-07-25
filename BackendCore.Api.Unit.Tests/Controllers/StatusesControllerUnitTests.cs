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
        private readonly Mock<IStatusService> _lookupServiceMock;
        private readonly StatusesController _controller;
        public StatusesControllerUnitTests()
        {
            _lookupServiceMock = new Mock<IStatusService>();
            Fixture.Register(() => _lookupServiceMock.Object);

            _controller = new StatusesController(_lookupServiceMock.Object);
        }

        [Fact]
        public async Task GetStatusesAsync_Return_Ok()
        {
            // AAA
            //Arrange (set up variables 

            var result = (IFinalResult)Fixture.Build<Result>().With(p => p.Status, HttpStatusCode.OK).Create();

            _lookupServiceMock.Setup(x => x.GetStatusesAsync())
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetStatusesAsync();


            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }


        
    }
}