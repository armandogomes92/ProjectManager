using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;
using ProjectManagerApi.Infrastructure.Repositories;

namespace ProjectManagerApi.Tests.Repositories;

public class CommentTest
{
    private readonly AppDbContext _context;
    private readonly CommentRepository _commentRepository;

    public CommentTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _commentRepository = new CommentRepository(_context);
    }

    [Fact]
    public async System.Threading.Tasks.Task SaveComment_ShouldReturnTrue_WhenCommentIsSaved()
    {

        ResetDatabase().Wait();

        var comment = new Comentario
        {
            Id = 1,
            TextoComentario = "Test Comment",
            DataCriacao = DateTime.Now,
            TarefaId = 1,
            UsuarioId = 1
        };

        var result = await _commentRepository.SaveComment(comment);

        Assert.True(result);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetCommentsByTaskId_ShouldReturnComments_WhenCommentsExist()
    {
        ResetDatabase().Wait();

        var comments = new List<Comentario>
        {
            new Comentario { Id = 1, TextoComentario = "Test Comment 1", TarefaId = 1, UsuarioId = 1 },
            new Comentario { Id = 2, TextoComentario = "Test Comment 2", TarefaId = 1, UsuarioId = 2 }
        };

        _context.Comments.AddRange(comments);
        await _context.SaveChangesAsync();

        var result = await _commentRepository.GetCommentsByTaskId(1);

        Assert.Equal(2, result.Count());
    }
    private async System.Threading.Tasks.Task ResetDatabase()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();
    }
}