using Moq;
using ProjectManagerApi.Application.Handlers.Project.Queries;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class GetAllProjectsTests
{
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly GetAllProjectsByUserIdHandler _handler;

    public GetAllProjectsTests()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _handler = new GetAllProjectsByUserIdHandler(_mockProjectRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnAllActiveProjects()
    {
        var projects = new List<Projeto>
        {
            new Projeto { Id = 1, Nome = "Project 1", Active = true, UserId = 1 },
            new Projeto { Id = 2, Nome = "Project 2", Active = true, UserId = 1 }
        };

        _mockProjectRepository.Setup(repo => repo.GetAllProjects(It.IsAny<int>())).ReturnsAsync(projects);

        var query = new GetAllProjectsByUserIdQuery { UserId = 1 };

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(projects, result.Content);
    }
}