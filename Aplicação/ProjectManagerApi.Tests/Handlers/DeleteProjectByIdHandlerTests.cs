using Moq;
using ProjectManagerApi.Application.Handlers.Project.Commands;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class DeleteProjectByIdHandlerTests
{
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly DeleteProjectByIdHandler _handler;

    public DeleteProjectByIdHandlerTests()
    {
        _mockProjectRepository = new Mock<IProjectRepository>();
        _mockTaskRepository = new Mock<ITaskRepository>();
        _handler = new DeleteProjectByIdHandler(_mockProjectRepository.Object, _mockTaskRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnProjectHasPendingTasks_WhenProjectHasPendingTasks()
    {
        _mockTaskRepository.Setup(repo => repo.CheckIfPendingTasksByProjectId(It.IsAny<int>())).ReturnsAsync(true);

        var command = new DeleteProjectByIdCommand
        {
            ProjectId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.ProjectHasPendingTasks, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnProjectNotFound_WhenProjectDoesNotExist()
    {
        _mockTaskRepository.Setup(repo => repo.CheckIfPendingTasksByProjectId(It.IsAny<int>())).ReturnsAsync(false);
        _mockProjectRepository.Setup(repo => repo.GetProjectById(It.IsAny<int>())).ReturnsAsync((Projeto)null);

        var command = new DeleteProjectByIdCommand
        {
            ProjectId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.ProjectNotFound, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnProjectDisabled_WhenProjectIsDisabled()
    {
        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };
        _mockTaskRepository.Setup(repo => repo.CheckIfPendingTasksByProjectId(It.IsAny<int>())).ReturnsAsync(false);
        _mockProjectRepository.Setup(repo => repo.GetProjectById(It.IsAny<int>())).ReturnsAsync(project);

        var command = new DeleteProjectByIdCommand
        {
            ProjectId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.ProjectDisabled, result.Content);
    }
}