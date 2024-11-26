using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;
using ProjectManagerApi.Infrastructure.Repositories;

namespace ProjectManagerApi.Tests.Repositories;

public class TaskTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public TaskTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async System.Threading.Tasks.Task GetAllTasksByProjectId_ShouldReturnTasksForActiveProject()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };
        var tasks = new List<Domain.Models.DataModels.Tarefa>
        {
            new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", ProjetoId = 1, Projeto = project, Descricao = "Teste descrição" },
            new Domain.Models.DataModels.Tarefa { Id = 2, Titulo = "Task 2", ProjetoId = 1, Projeto = project, Descricao = "Teste descrição" }
        };

        context.Projetos.Add(project);
        context.Tasks.AddRange(tasks);
        await context.SaveChangesAsync();

        var result = await repository.GetAllTasksByProjectId(1);

        Assert.Equal(2, result.Count());
        Assert.Contains(result, t => t.Titulo == "Task 1");
        Assert.Contains(result, t => t.Titulo == "Task 2");
    }

    [Fact]
    public async System.Threading.Tasks.Task GetTaskById_ShouldReturnCorrectTask()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", Descricao = "Teste descrição" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var result = await repository.GetTaskById(1);

        Assert.Equal("Task 1", result.Titulo);
    }

    [Fact]
    public void CreateTask_ShouldAddTask()
    {
        using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        ResetDatabase(context).Wait();

        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", Descricao = "Teste descrição" };

        var result = repository.CreateTask(task);

        Assert.True(result);
        Assert.Equal(1, context.Tasks.Count());
        Assert.Equal("Task 1", context.Tasks.First().Titulo);
    }

    [Fact]
    public async System.Threading.Tasks.Task UpdateTaskById_ShouldUpdateTask()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", Descricao = "Teste descrição" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        task.Titulo = "Updated Task 1";
        var result = await repository.UpdateTaskById(task);

        Assert.True(result);
        Assert.Equal("Updated Task 1", context.Tasks.First().Titulo);
    }

    [Fact]
    public void DeleteTaskById_ShouldRemoveTask()
    {
        using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        ResetDatabase(context).Wait();

        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", Descricao = "Teste descrição" };
        context.Tasks.Add(task);
        context.SaveChanges();

        var result = repository.DeleteTaskById(task);

        Assert.True(result);
        Assert.Equal(0, context.Tasks.Count());
    }

    [Fact]
    public async System.Threading.Tasks.Task CheckIfPendingTasksByProjectId_ShouldReturnTrueIfPendingTasksExist()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var task = new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", ProjetoId = 1, Status = StatusEnum.Pendente, Descricao = "Teste descrição" };
        context.Tasks.Add(task);
        await context.SaveChangesAsync();

        var result = await repository.CheckIfPendingTasksByProjectId(1);

        Assert.True(result);
    }

    [Fact]
    public async System.Threading.Tasks.Task CanBeAddTask_ShouldReturnTrueIfTasksCountIsLessThanOrEqualTo20()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var tasks = new List<Domain.Models.DataModels.Tarefa>();
        for (int i = 1; i <= 20; i++)
        {
            tasks.Add(new Domain.Models.DataModels.Tarefa { Id = i, Titulo = $"Task {i}", ProjetoId = 1, Descricao = "Teste descrição" });
        }

        context.Tasks.AddRange(tasks);
        await context.SaveChangesAsync();

        var result = await repository.CanBeAddTask(1);

        Assert.True(result);
    }

    [Fact]
    public async System.Threading.Tasks.Task RegisterHistoryTask_ShouldUpdateTaskHistory()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var taskHistory = new HistoricoDaTarefa { Id = 1, ItemModificado = "Titulo", ValoresAntigos = "Old Title", ValoresNovos = "New Title", DataModificacao = DateTime.Now, TaskId = 1 };
        context.TaskHistories.Add(taskHistory);
        await context.SaveChangesAsync();

        taskHistory.ValoresNovos = "Updated New Title";
        var result = await repository.RegisterHistoryTask(taskHistory);

        Assert.True(result);
        Assert.Equal("Updated New Title", context.TaskHistories.First().ValoresNovos);
    }

    [Fact]
    public async System.Threading.Tasks.Task AddComment_ShouldAddCommentToTask()
    {
        await using var context = new AppDbContext(_options);
        var repository = new TaskRepository(context);
        await ResetDatabase(context);

        var comment = new Comentario { Id = 1, TextoComentario = "This is a comment", TarefaId = 1, UsuarioId = 1, DataCriacao = DateTime.Now };
        var result = await repository.AddComment(comment);

        Assert.True(result);
        Assert.Equal(1, context.Comments.Count());
        Assert.Equal("This is a comment", context.Comments.First().TextoComentario);
    }

    private async System.Threading.Tasks.Task ResetDatabase(AppDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}