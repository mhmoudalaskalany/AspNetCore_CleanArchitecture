using System.Linq.Expressions;
using System.Net;
using AutoFixture;
using AutoMapper;
using BackendCore.Common.DTO.Lookup.Status;
using BackendCore.Common.Extensions;
using BackendCore.Common.Infrastructure.UnitOfWork;
using BackendCore.Entities.Entities.Lookup;
using BackendCore.Service.Services.Base;
using BackendCore.Service.Services.Lookups.Status;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace BackendCore.Service.Unit.Tests.Service
{
    public class StatusServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Status>> _uowMock;
        private readonly StatusService _statusServiceMock;
        private readonly Mock<IServiceBaseParameter<Status>> _baseParamsMock;
        private readonly Mock<IMapper> _mapperMock;
        public StatusServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Status>>();
            Fixture.Register(() => _uowMock.Object);

            _mapperMock = new Mock<IMapper>();
            Fixture.Register(() => _mapperMock.Object);

            _baseParamsMock = new Mock<IServiceBaseParameter<Status>>().SetupAllProperties();
            Fixture.Register(() => _baseParamsMock.Object);

            _baseParamsMock.Object.UnitOfWork = _uowMock.Object;
            _baseParamsMock.Object.Mapper = _mapperMock.Object;

            _statusServiceMock = new StatusService(_baseParamsMock.Object);
        }

        [Fact]
        public async Task GetStatusesAsync_ReturnList()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();
            
            _uowMock.Setup(x =>
                x.Repository.FindAsync(It.IsAny<Expression<Func<Status, bool>>>(),
                It.IsAny<IEnumerable<SortModel>>(), It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(), It.IsAny<bool>())).Returns(Task.FromResult(entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, List<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            // act
            var result = await _statusServiceMock.GetStatusesAsync();
            var list = (List<StatusDto>)result.Data;

            // assert
            Assert.True(list.Any());
            Assert.Equal(HttpStatusCode.OK , result.Status);
            Assert.Null(result.Exception);
            Assert.Equal("Success" , result.Message);
        }
    }
}
