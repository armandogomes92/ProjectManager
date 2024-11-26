using ProjectManagerApi.Domain.Models.DataModels;

namespace ProjectManagerApi.Infrastructure.Contracts;

public interface ICommentRepository
{
    Task<bool> SaveComment(Comentario comment);
    Task<IEnumerable<Comentario>> GetCommentsByTaskId(int taskId);
}