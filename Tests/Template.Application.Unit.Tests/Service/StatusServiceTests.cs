using System.Linq.Expressions;
using System.Net;
using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Moq;
using Template.Application.Services.Base;
using Template.Application.Services.Lookups.Status;
using Template.Common.Core;
using Template.Common.DTO.Lookup.Status;
using Template.Common.Extensions;
using Template.Common.Infrastructure.UnitOfWork;
using Template.Domain.Entities.Lookup;

namespace Template.Application.Unit.Tests.Service
{
    public class StatusServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Status>> _uowMock;
        private readonly Mock<ILogger<StatusService>> _loggerMock;
        private readonly StatusService _statusServiceMock;
        private readonly Mock<IServiceBaseParameter<Status>> _baseParamsMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IResponseResult> _responseResultMock;
        public StatusServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Status>>();
            Fixture.Register(() => _uowMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

            _loggerMock = new Mock<ILogger<StatusService>>();
            Fixture.Register(() => _loggerMock.Object);

            _responseResultMock = new Mock<IResponseResult>();
            Fixture.Register(() => _responseResultMock.Object);

            _baseParamsMock = new Mock<IServiceBaseParameter<Status>>().SetupAllProperties();

            Fixture.Register(() => _baseParamsMock.Object);

            _baseParamsMock.Object.UnitOfWork = _uowMock.Object;

            _baseParamsMock.Object.Mapper = _mapperMock.Object;

            _baseParamsMock.Object.Logger = _loggerMock.Object;

            _baseParamsMock.Object.ResponseResult = _responseResultMock.Object;

            _statusServiceMock = new StatusService(_baseParamsMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList_Without_Predicate()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();


            _uowMock.Setup(x =>
                x.Repository.GetAllAsync(
                    It.IsAny<IEnumerable<SortModel>>(), It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(), It.IsAny<bool>()))
                .ReturnsAsync(entities);

            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, List<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            var finalResult = new Mock<IFinalResult>();
            finalResult.SetupAllProperties();
            finalResult.Object.Data = mapped;
            finalResult.Object.Status = HttpStatusCode.OK;
            finalResult.Object.Message = "Success";

            _responseResultMock.Setup(x => x.PostResult(It.IsAny<object>(), It.IsAny<HttpStatusCode>(), It.IsAny<Exception>(), It.IsAny<string>()))
                .Returns(finalResult.Object);

            // act
            var result = await _statusServiceMock.GetAllAsync();
            var list = (List<StatusDto>)result.Data;

            // assert
            Assert.True(list.Any());
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Null(result.Exception);
            Assert.Equal("Success", result.Message);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList_With_Predicate()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();

            Expression<Func<Status, bool>> predicate = Fixture.Create<Expression<Func<Status, bool>>>();
            _uowMock.Setup(x =>
                    x.Repository.FindAsync(It.IsAny<Expression<Func<Status, bool>>>(),
                        It.IsAny<IEnumerable<SortModel>>(), It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(), It.IsAny<bool>()))
                .ReturnsAsync(entities);


            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            var finalResult = new Mock<IFinalResult>();
            finalResult.SetupAllProperties();
            finalResult.Object.Data = mapped;
            finalResult.Object.Status = HttpStatusCode.OK;
            finalResult.Object.Message = "Success";

            _responseResultMock.Setup(x => x.PostResult(It.IsAny<object>(), It.IsAny<HttpStatusCode>(), It.IsAny<Exception>(), It.IsAny<string>()))
                .Returns(finalResult.Object);

            // act
            var result = await _statusServiceMock.GetAllAsync(predicate: predicate);
            var list = (List<StatusDto>)result.Data;

            // assert
            Assert.True(list.Any());
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.Null(result.Exception);
            Assert.Equal("Success", result.Message);
        }
    }
}
