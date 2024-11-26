using Moq;
using ProjectManagerApi.Application.Handlers.Tasks.Queries;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Tests.Handlers;

public class GetTasksByProjectIdHandlerTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly GetTasksByProjectIdHandler _handler;

    public GetTasksByProjectIdHandlerTests()
    {
        _mockTaskRepository = new Mock<ITaskRepository>();
        _handler = new GetTasksByProjectIdHandler(_mockTaskRepository.Object);
    }

    [Fact]
    public async System.Threading.Tasks.Task Handle_ShouldReturnTasksForGivenProjectId()
    {
        var tasks = new List<Domain.Models.DataModels.Tarefa>
        {
            new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", ProjetoId = 1 },
            new Domain.Models.DataModels.Tarefa { Id = 2, Titulo = "Task 2", ProjetoId = 1 }
        };

        _mockTaskRepository.Setup(repo => repo.GetAllTasksByProjectId(It.IsAny<int>())).ReturnsAsync(tasks);

        var query = new GetTasksByProjectIdQuery
        {
            ProjectId = 1
        };

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(tasks, result.Content);
    }
}