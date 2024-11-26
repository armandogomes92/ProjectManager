using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;
using ProjectManagerApi.Infrastructure.Repositories;

namespace ProjectManagerApi.Tests.Repositories;

public class ProjectTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public ProjectTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async System.Threading.Tasks.Task GetAllProjects_ShouldReturnActiveProjects()
    {
        await using var context = new AppDbContext(_options);
        var repository = new ProjectRepository(context);
        await ResetDatabase(context);
        
        int userId = 1;
        var projects = new List<Projeto>
        {
            new Projeto { Id = 7, Nome = "Project 1", Active = true, UserId = 1 },
            new Projeto { Id = 8, Nome = "Project 2", Active = false, UserId = 1 },
            new Projeto { Id = 9, Nome = "Project 3", Active = true, UserId = 1 }
        };

        context.Projetos.AddRange(projects);
        await context.SaveChangesAsync();

        var result = await repository.GetAllProjects(userId);

        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Nome == "Project 1");
        Assert.Contains(result, p => p.Nome == "Project 3");
    }

    [Fact]
    public async System.Threading.Tasks.Task GetProjectById_ShouldReturnCorrectProject()
    {
        await using var context = new AppDbContext(_options);
        var repository = new ProjectRepository(context);
        await ResetDatabase(context);

        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };
        context.Projetos.Add(project);
        await context.SaveChangesAsync();

        var result = await repository.GetProjectById(1);

        Assert.Equal("Project 1", result.Nome);
    }

    [Fact]
    public void CreateProject_ShouldAddProject()
    {
        using var context = new AppDbContext(_options);
        var repository = new ProjectRepository(context);
        ResetDatabase(context).Wait();

        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };

        var result = repository.CreateProject(project);

        Assert.True(result);
        Assert.Equal(1, context.Projetos.Count());
        Assert.Equal("Project 1", context.Projetos.First().Nome);
    }

    [Fact]
    public void DisableProject_ShouldUpdateProject()
    {
        using var context = new AppDbContext(_options);
        var repository = new ProjectRepository(context);
        ResetDatabase(context).Wait();

        var project = new Projeto { Id = 1, Nome = "Project 1", Active = true };
        context.Projetos.Add(project);
        context.SaveChanges();
        project.Active = false;

        var result = repository.DisableProject(project);

        Assert.True(result);
        Assert.False(context.Projetos.First().Active);
    }

    private async System.Threading.Tasks.Task ResetDatabase(AppDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}