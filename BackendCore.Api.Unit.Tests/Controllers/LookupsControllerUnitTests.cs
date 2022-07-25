using System.Net;
using AutoFixture;
using BackendCore.Api.Controllers.Lookup;
using BackendCore.Common.Core;
using BackendCore.Service.Services.Lookups;
using Moq;

namespace BackendCore.Api.Unit.Tests.Controllers
{
    public class LookupsControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<ILookupService> _lookupServiceMock;
        private readonly LookupsController _controller;
        public LookupsControllerUnitTests()
        {
            _lookupServiceMock = new Mock<ILookupService>();
            Fixture.Register(() => _lookupServiceMock.Object);

            _controller = new LookupsController(_lookupServiceMock.Object);
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


        [Fact]
        public async Task GetActionsAsync_Return_Ok()
        {
            // AAA
            //Arrange (set up variables 

            var result = (IFinalResult)Fixture.Build<Result>().With(p => p.Status, HttpStatusCode.OK).Create();

            _lookupServiceMock.Setup(x => x.GetActionsAsync())
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetActionsAsync();


            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }

        
    }
}