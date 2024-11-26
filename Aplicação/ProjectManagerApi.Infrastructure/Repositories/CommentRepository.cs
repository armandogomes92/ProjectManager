using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;

namespace ProjectManagerApi.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveComment(Comentario comment)
    {
        _context.Comments.Add(comment);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Comentario>> GetCommentsByTaskId(int taskId)
    {
        return await _context.Comments.Where(c => c.TarefaId == taskId).ToListAsync();
    }
}
