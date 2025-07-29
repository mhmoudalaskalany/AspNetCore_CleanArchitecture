using System.Linq.Expressions;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Template.Api.Controllers.V1.Lookup;
using Template.Application.Services.Lookups.Category;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Category;
using Template.Domain.Entities.Lookup;

namespace Template.Api.Unit.Tests.Controllers.V1.Lookup.Categories
{
    public class CategoriesControllerUnitTests : AutoFixtureBase
    {
        private readonly Mock<ICategoryService> _categoryServiceMock;
        private readonly CategoriesController _controller;
        public CategoriesControllerUnitTests()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
            Fixture.Register(() => _categoryServiceMock.Object);
            _controller = new CategoriesController(_categoryServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_Return_Ok()
        {
            // AAA
            //Arrange (set up variables) 
            var testDtos = Fixture.CreateMany<CategoryDto>().ToList();
            var result = Result<IEnumerable<CategoryDto>>.Success(testDtos);

            _categoryServiceMock.Setup(x => x.GetAllAsync(It.IsAny<bool>(), It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(result);

            //Act ( execute the target method )
            var actionResult = await _controller.GetAllAsync();

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, objectResult.StatusCode);

            var apiResponse = Assert.IsType<ApiResponse<IEnumerable<CategoryDto>>>(objectResult.Value);
            Assert.Equal((int)HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.NotNull(apiResponse.Data);
        }




    }
}