using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;
using ProjectManagerApi.Infrastructure.Repositories;

namespace ProjectManagerApi.Tests.Repositories;

public class UserTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public UserTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
    }

    [Fact]
    public async System.Threading.Tasks.Task GetUserById_ShouldReturnCorrectUser()
    {
        await using var context = new AppDbContext(_options);
        var repository = new UserRepository(context);
        await ResetDatabase(context);

        var user = new Usuario { Id = 1, Nome = "User 1", Cargo = PositionEnum.Gerente };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var result = await repository.GetUserById(1);

        Assert.Equal("User 1", result.Nome);
        Assert.Equal(PositionEnum.Gerente, result.Cargo);
    }

    private async System.Threading.Tasks.Task ResetDatabase(AppDbContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}