using Moq;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class AddTasksByProjectIdHandlerTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly Mock<IProjectRepository> _mockProjectRepository;
    private readonly AddTasksByProjectIdHandler _handler;

    public AddTasksByProjectIdHandlerTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _mockProjectRepository = new Mock<IProjectRepository>();
        _handler = new AddTasksByProjectIdHandler(_mockTaskRepository.Object, _mockProjectRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnProjectNotFound_WhenProjectDoesNotExist()
    {
        _mockProjectRepository.Setup(repo => repo.GetProjectById(It.IsAny<int>())).ReturnsAsync((Projeto)null);

        var command = new AddTasksByProjectIdCommand
        {
            ProjetoId = 1,
            Titulo = "New Task",
            Descricao = "Task Description",
            PrazoConclusao = DateTime.Now.AddDays(7),
            Prioridade = PriorityEnum.Medium
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.ProjectNotFound, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnTaskNotCanBeAdded_WhenTaskLimitExceeded()
    {
        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };
        _mockProjectRepository.Setup(repo => repo.GetProjectById(It.IsAny<int>())).ReturnsAsync(project);
        _mockTaskRepository.Setup(repo => repo.CanBeAddTask(It.IsAny<int>())).ReturnsAsync(false);

        var command = new AddTasksByProjectIdCommand
        {
            ProjetoId = 1,
            Titulo = "New Task",
            Descricao = "Task Description",
            PrazoConclusao = DateTime.Now.AddDays(7),
            Prioridade = PriorityEnum.Medium
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskNotCanBeAdded, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldCreateTask_WhenProjectExistsAndTaskCanBeAdded()
    {
        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };
        _mockProjectRepository.Setup(repo => repo.GetProjectById(It.IsAny<int>())).ReturnsAsync(project);
        _mockTaskRepository.Setup(repo => repo.CanBeAddTask(It.IsAny<int>())).ReturnsAsync(true);
        _mockTaskRepository.Setup(repo => repo.CreateTask(It.IsAny<Domain.Models.DataModels.Tarefa>())).Returns(true);

        var command = new AddTasksByProjectIdCommand
        {
            ProjetoId = 1,
            Titulo = "New Task",
            Descricao = "Task Description",
            PrazoConclusao = DateTime.Now.AddDays(7),
            Prioridade = PriorityEnum.Medium
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskCreated, result.Content);
        _mockTaskRepository.Verify(repo => repo.CreateTask(It.IsAny<Domain.Models.DataModels.Tarefa>()), Times.Once);
    }
}