using Moq;
using ProjectManagerApi.Application.Handlers.Report.Queries;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Domain.Models.DTO;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class GetReportsFromTheLast30DaysHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IReportRepository> _mockReportRepository;
    private readonly GetReportsFromTheLast30DaysHandler _handler;

    public GetReportsFromTheLast30DaysHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockReportRepository = new Mock<IReportRepository>();
        _handler = new GetReportsFromTheLast30DaysHandler(_mockUserRepository.Object, _mockReportRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnUserNotFound_WhenUserDoesNotExist()
    {
        _mockUserRepository.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync((Usuario)null);

        var command = new GetReportsFromTheLast30DaysCommand
        {
            UserId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.UserNotFound, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnPositionHasNoAccess_WhenUserIsNotManager()
    {
        var user = new Usuario { Id = 1, Nome = "User 1", Cargo = PositionEnum.Analista };
        _mockUserRepository.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync(user);

        var command = new GetReportsFromTheLast30DaysCommand
        {
            UserId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.PositionHasNoAccess, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnUserPerformanceReport_WhenUserIsManager()
    {
        var user = new Usuario { Id = 1, Nome = "User 1", Cargo = PositionEnum.Gerente };
        var reports = new List<UserPerformanceReportDto>
        {
            new UserPerformanceReportDto { UserId = 1, UserName = "User 1", AverageTasksCompleted = 5.0 }
        };

        _mockUserRepository.Setup(repo => repo.GetUserById(It.IsAny<int>())).ReturnsAsync(user);
        _mockReportRepository.Setup(repo => repo.GetUserPerformanceReportAsync()).ReturnsAsync(reports);

        var command = new GetReportsFromTheLast30DaysCommand
        {
            UserId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(reports, result.Content);
    }
}