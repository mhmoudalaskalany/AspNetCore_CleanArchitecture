using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Template.Application.Services.Base;
using Template.Application.Services.Lookups.Status;
using Template.Common.Core;
using Template.Common.DTO.Base;
using Template.Common.DTO.Lookup.Status;
using Template.Common.DTO.Lookup.Status.Parameters;
using Template.Common.Extensions;
using Template.Common.Infrastructure.UnitOfWork;
using Template.Domain;
using Template.Domain.Entities.Lookup;

namespace Template.Application.Unit.Tests.Service
{
    public class StatusServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Status>> _uowMock;
        private readonly StatusService _statusService;
        private readonly Mock<IServiceBaseParameter<Status>> _baseParamsMock;
        private readonly Mock<IMapper> _mapperMock;

        public StatusServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Status>>();
            _mapperMock = new Mock<IMapper>();
            _baseParamsMock = new Mock<IServiceBaseParameter<Status>>().SetupAllProperties();

            _baseParamsMock.Object.UnitOfWork = _uowMock.Object;
            _baseParamsMock.Object.Mapper = _mapperMock.Object;

            _statusService = new StatusService(_baseParamsMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList_Without_Predicate()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();

            _uowMock.Setup(x =>
                x.Repository.GetAllAsync(
                    It.IsAny<IEnumerable<SortModel>>(), 
                    It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(), 
                    It.IsAny<bool>()))
                .ReturnsAsync(entities);

            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            // act
            var result = await _statusService.GetAllAsync();

            // assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Any());
            Assert.NotNull(result.Errors);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task GetAllAsync_ReturnList_With_Predicate()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();

            Expression<Func<Status, bool>> predicate = x => x.Id > 0;

            _uowMock.Setup(x =>
                    x.Repository.FindAsync(It.IsAny<Expression<Func<Status, bool>>>(),
                        It.IsAny<IEnumerable<SortModel>>(), 
                        It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(), 
                        It.IsAny<bool>()))
                .ReturnsAsync(entities);

            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            // act
            var result = await _statusService.GetAllAsync(predicate: predicate);

            // assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Any());
            Assert.NotNull(result.Errors);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task GetAllPagedAsync_ReturnPagedResult()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();
            var filter = Fixture.Build<BaseParam<StatusFilter>>()
                .With(x => x.PageNumber, 1)
                .With(x => x.PageSize, 10)
                .With(x => x.Filter, new StatusFilter { IsDeleted = false })
                .Create();
            
            var totalCount = 25;

            _uowMock.Setup(x =>
                    x.Repository.FindPagedAsync(
                        It.IsAny<Expression<Func<Status, bool>>>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<IEnumerable<SortModel>>(),
                        It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(),
                        It.IsAny<bool>()))
                .ReturnsAsync((totalCount, entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            // act
            var result = await _statusService.GetAllPagedAsync(filter);

            // assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Any());
            Assert.Equal(totalCount, result.TotalCount);
            Assert.Equal(MessagesConstants.Success, result.Message);
        }

        [Fact]
        public async Task GetDropDownAsync_ReturnPagedResult()
        {
            // arrange
            var entities = Fixture.Build<Status>().CreateMany();
            var mapped = Fixture.Build<StatusDto>().CreateMany().ToList();
            var filter = Fixture.Build<BaseParam<SearchCriteriaFilter>>()
                .With(x => x.PageNumber, 1)
                .With(x => x.PageSize, 10)
                .With(x => x.Filter, new SearchCriteriaFilter { SearchCriteria = "test" })
                .Create();
            
            var totalCount = 15;

            _uowMock.Setup(x =>
                    x.Repository.FindPagedAsync(
                        It.IsAny<Expression<Func<Status, bool>>>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<IEnumerable<SortModel>>(),
                        It.IsAny<Func<IQueryable<Status>, IIncludableQueryable<Status, object>>>(),
                        It.IsAny<bool>()))
                .ReturnsAsync((totalCount, entities));

            _mapperMock.Setup(x => x.Map<IEnumerable<Status>, IEnumerable<StatusDto>>(It.IsAny<IEnumerable<Status>>()))
                .Returns(mapped);

            // act
            var result = await _statusService.GetDropDownAsync(filter);

            // assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.True(result.Data.Any());
            Assert.Equal(totalCount, result.TotalCount);
            Assert.Equal(MessagesConstants.Success, result.Message);
        }

        [Fact]
        public async Task DeleteRangeAsync_WithValidIds_ReturnSuccess()
        {
            // arrange
            var ids = new List<int> { 1, 2, 3 };
            var affectedRows = 3;

            _uowMock.Setup(x =>
                    x.Repository.RemoveBulkAsync(It.IsAny<Expression<Func<Status, bool>>>()))
                .ReturnsAsync(affectedRows);

            // act
            var result = await _statusService.DeleteRangeAsync(ids);

            // assert
            Assert.True(result.IsSuccess);
            Assert.Equal(MessagesConstants.DeleteSuccess, result.Message);
        }

        [Fact]
        public async Task DeleteRangeAsync_WithNoRowsAffected_ReturnFailure()
        {
            // arrange
            var ids = new List<int> { 1, 2, 3 };
            var affectedRows = 0;

            _uowMock.Setup(x =>
                    x.Repository.RemoveBulkAsync(It.IsAny<Expression<Func<Status, bool>>>()))
                .ReturnsAsync(affectedRows);

            // act
            var result = await _statusService.DeleteRangeAsync(ids);

            // assert
            Assert.False(result.IsSuccess);
            Assert.Equal(MessagesConstants.DeleteError, result.Message);
        }
    }
}
