using Moq;
using ProjectManagerApi.Application.Handlers.Project.Commands;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class AddProjectHandlerTests
{
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly AddProjectHandler _handler;

    public AddProjectHandlerTests()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _handler = new AddProjectHandler(_mockProjectRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldCreateProject()
    {
        var command = new AddProjectCommand
        {
            Id = 1,
            Nome = "New Project",
            UserId = 1
        };

        _mockProjectRepository.Setup(repo => repo.CreateProject(It.IsAny<Projeto>())).Returns(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        _mockProjectRepository.Verify(repo => repo.CreateProject(It.IsAny<Projeto>()), Times.Once);
    }
}