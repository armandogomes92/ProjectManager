using Moq;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Delete;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class DeleteTaskByIdHandlerTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly DeleteTaskByIdHandler _handler;

    public DeleteTaskByIdHandlerTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _handler = new DeleteTaskByIdHandler(_mockTaskRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnTaskNotFound_WhenTaskDoesNotExist()
    {
        _mockTaskRepository.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync((Domain.Models.DataModels.Tarefa)null);

        var command = new DeleteTaskByIdCommand
        {
            Id = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskNotFound, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldDeleteTask_WhenTaskExists()
    {
        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1" };
        _mockTaskRepository.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync(task);
        _mockTaskRepository.Setup(repo => repo.DeleteTaskById(It.IsAny<Domain.Models.DataModels.Tarefa>())).Returns(true);

        var command = new DeleteTaskByIdCommand
        {
            Id = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskDeleted, result.Content);
        _mockTaskRepository.Verify(repo => repo.DeleteTaskById(It.IsAny<Domain.Models.DataModels.Tarefa>()), Times.Once);
    }
}