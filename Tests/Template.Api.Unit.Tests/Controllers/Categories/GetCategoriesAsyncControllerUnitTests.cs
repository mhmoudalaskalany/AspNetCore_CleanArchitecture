using System.Net;
using AutoFixture;
using Moq;
using Template.Api.Controllers.Lookup;
using Template.Application.Services.Lookups.Category;
using Template.Common.Core;

namespace Template.Api.Unit.Tests.Controllers.Categories
{
    public class GetCategoriesAsyncControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoriesController _controller;
        public GetCategoriesAsyncControllerUnitTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            Fixture.Register(() => _categoryServiceMock.Object);
            _controller = new CategoriesController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task GetActionsAsync_Return_Ok()
        {
            //Arrange (set up variables 
            var result = (IFinalResult)Fixture.Build<FinalResult>().With(p => p.Status, HttpStatusCode.OK).Create();
            _categoryServiceMock.Setup(x => x.GetCategoriesAsync())
                .Returns(Task.FromResult(result));

            //Act ( execute the target method )
            var finalResult = await _controller.GetCategoriesAsync();

            //Assert
            Assert.Equal(HttpStatusCode.OK, finalResult.Status);
        }



    }
}