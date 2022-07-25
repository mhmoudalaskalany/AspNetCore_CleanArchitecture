using System.Linq.Expressions;
using AutoFixture;
using BackendCore.Common.Core;
using BackendCore.Common.Extensions;
using BackendCore.Common.Infrastructure.Repository;
using BackendCore.Common.Infrastructure.UnitOfWork;
using BackendCore.Entities.Entities.Lookup;
using BackendCore.Service.Services.Lookups.Status;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace BackendCore.Service.Unit.Tests.Service
{
    public class StatusServiceTests : AutoFixtureBase
    {
        private readonly Mock<IUnitOfWork<Status>> _uowMock;
        private readonly Mock<IRepository<Status>> _repoMock;
        private readonly Mock<StatusService> _statusServiceMock;
        public StatusServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork<Status>>();
            _repoMock = new Mock<IRepository<Status>>();
            _statusServiceMock = new Mock<StatusService>();
        }

        public async Task GetStatusesAsync_ReturnList()
        {
            // arrange
            var result = Fixture.Build<Status>().CreateMany();
            var responseResult = Fixture.Create<ResponseResult>();

            _repoMock.Setup(x => 
                x.FindAsync(It.IsAny<Expression<Func<Status, bool>>>(), 
                It.IsAny<IEnumerable<SortModel>>(), 
                include: It.IsAny<IIncludableQueryable<Status ,Status>(), It.IsAny<bool>())).Returns(Task.FromResult(result));
        }
    }
}
