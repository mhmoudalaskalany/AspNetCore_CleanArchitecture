using System.Net;
using AutoFixture;
using Moq;
using Template.Api.Controllers.V1.Lookup;
using Template.Application.Services.Lookups.Status;
using Template.Common.Core;

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
        public async Task GetAllAsync_Return_Ok()
        {
            //Arrange (set up variables)
            var result = (IFinalResult)Fixture.Build<FinalResult>().With(p => p.Status, HttpStatusCode.OK).Create();
            _statusServiceMock.Setup(x => x.GetAllAsync(It.IsAny<bool>() , null))
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetAllAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }



    }
}