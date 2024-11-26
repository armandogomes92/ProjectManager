using Moq;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class AddComentByTaskIdTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly AddComentByTaskIdHandler _handler;

    public AddComentByTaskIdTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _handler = new AddComentByTaskIdHandler(_mockTaskRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnTaskNotFound_WhenTaskDoesNotExist()
    {
        var a = _mockTaskRepository.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync((Domain.Models.DataModels.Tarefa)null);

        var command = new AddComentByTaskIdCommand
        {
            TaskId = 1,
            TextOfComment = "Test comment",
            UserId = 1
        };
        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.TaskNotFound, result.Content);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldAddCommentAndRegisterHistory_WhenTaskExists()
    {
        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", Descricao = "Teste descrição" };
        _mockTaskRepository.Setup(repo => repo.GetTaskById(It.IsAny<int>())).ReturnsAsync(task);
        _mockTaskRepository.Setup(repo => repo.AddComment(It.IsAny<Comentario>())).ReturnsAsync(true);
        _mockTaskRepository.Setup(repo => repo.RegisterHistoryTask(It.IsAny<HistoricoDaTarefa>())).ReturnsAsync(true);

        var command = new AddComentByTaskIdCommand
        {
            TaskId = 1,
            TextOfComment = "Test comment",
            UserId = 1
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(Messages.CommentAdded, result.Content);
        _mockTaskRepository.Verify(repo => repo.AddComment(It.IsAny<Comentario>()), Times.Once);
        _mockTaskRepository.Verify(repo => repo.RegisterHistoryTask(It.IsAny<HistoricoDaTarefa>()), Times.Once);
    }
}