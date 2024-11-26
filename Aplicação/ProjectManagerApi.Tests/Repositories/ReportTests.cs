using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;
using ProjectManagerApi.Infrastructure.Repositories;

namespace ProjectManagerApi.Tests.Repositories;

public class ReportTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public ReportTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async System.Threading.Tasks.Task GetUserPerformanceReportAsync_ShouldReturnCorrectReport()
    {
        await using var context = new AppDbContext(_options);
        var repository = new ReportRepository(context);
        await ResetDatabase(context);

        var users = new List<Usuario>
        {
            new Usuario { Id = 1, Nome = "User 1" },
            new Usuario { Id = 2, Nome = "User 2" }
        };

        var projects = new List<Projeto>
        {
            new Projeto { Id = 1, Nome = "Project 1", UserId = 1, Active = true },
            new Projeto { Id = 2, Nome = "Project 2", UserId = 2, Active = true }
        };

        var tasks = new List<Domain.Models.DataModels.Tarefa>
        {
            new Domain.Models.DataModels.Tarefa { Id = 1, Titulo = "Task 1", Status = StatusEnum.Concluída, UltimaAtualizacao = DateTime.Now.AddDays(-10), ProjetoId = 1, Descricao = "Teste descrição" },
            new Domain.Models.DataModels.Tarefa { Id = 2, Titulo = "Task 2", Status = StatusEnum.Concluída, UltimaAtualizacao = DateTime.Now.AddDays(-20), ProjetoId = 1, Descricao = "Teste descrição" },
            new Domain.Models.DataModels.Tarefa { Id = 3, Titulo = "Task 3", Status = StatusEnum.Concluída, UltimaAtualizacao = DateTime.Now.AddDays(-5), ProjetoId = 2, Descricao = "Teste descrição" }
        };

        context.Users.AddRange(users);
        context.Projetos.AddRange(projects);
        context.Tasks.AddRange(tasks);
        await context.SaveChangesAsync();

        var result = await repository.GetUserPerformanceReportAsync();

        Assert.Equal(2, result.Count());
        var user1Report = result.FirstOrDefault(r => r.UserId == 1);
        var user2Report = result.FirstOrDefault(r => r.UserId == 2);

        Assert.NotNull(user1Report);
        Assert.Equal("User 1", user1Report.UserName);
        Assert.Equal(2 / 30.0, user1Report.AverageTasksCompleted);

        Assert.NotNull(user2Report);
        Assert.Equal("User 2", user2Report.UserName);
        Assert.Equal(1 / 30.0, user2Report.AverageTasksCompleted);
    }

    private async System.Threading.Tasks.Task ResetDatabase(AppDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}