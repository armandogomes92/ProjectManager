using Moq;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Update;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class UpdateTaskByIdHandlerTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly UpdateTaskByIdHandler _handler;

    public UpdateTaskByIdHandlerTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _handler = new UpdateTaskByIdHandler(_mockTaskRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnTaskNotFound_WhenTaskDoesNotExist()
    {
        _mockTaskRepository.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync((Domain.Models.DataModels.Tarefa)null);

        var command = new UpdateTaskByIdCommand
        {
            Id = 1,
            Title = "Updated Task",
            Description = "Updated Description",
            Status = StatusEnum.EmAndamento,
            NewDeadLine = DateTime.Now.AddDays(7)
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskNotFound, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldUpdateTask_WhenTaskExists()
    {
        var task = new Domain.Models.DataModels.Tarefa
        {
            Id = 1,
            Titulo = "Task 1",
            Descricao = "Description 1",
            Status = StatusEnum.Pendente,
            PrazoConclusao = DateTime.Now.AddDays(5),
            UltimaAtualizacao = DateTime.Now
        };

        _mockTaskRepository.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync(task);
        _mockTaskRepository.Setup(repo => repo.UpdateTaskById(It.IsAny<Domain.Models.DataModels.Tarefa>())).ReturnsAsync(true);
        _mockTaskRepository.Setup(repo => repo.RegisterHistoryTask(It.IsAny<HistoricoDaTarefa>())).ReturnsAsync(true);

        var command = new UpdateTaskByIdCommand
        {
            Id = 1,
            Title = "Updated Task",
            Description = "Updated Description",
            Status = StatusEnum.EmAndamento,
            NewDeadLine = DateTime.Now.AddDays(7)
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskUpdated, result.Content);
        _mockTaskRepository.Verify(repo => repo.UpdateTaskById(It.IsAny<Domain.Models.DataModels.Tarefa>()), Times.Once);
        _mockTaskRepository.Verify(repo => repo.RegisterHistoryTask(It.IsAny<HistoricoDaTarefa>()), Times.Once);
    }
}