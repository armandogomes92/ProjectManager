using ProjectManagerApi.Domain.Models.DataModels;

namespace ProjectManagerApi.Infrastructure.Contracts;

public interface ITaskRepository
{
    Task<IEnumerable<Domain.Models.DataModels.Tarefa>> GetAllTasksByProjectId(int projetoId);
    Task<Domain.Models.DataModels.Tarefa> GetTaskById(int taskId);
    bool CreateTask(Domain.Models.DataModels.Tarefa task);
    Task<bool> UpdateTaskById(Domain.Models.DataModels.Tarefa task);
    bool DeleteTaskById(Domain.Models.DataModels.Tarefa task);
    Task<bool> CheckIfPendingTasksByProjectId(int projectId);
    Task<bool> CanBeAddTask(int projectId);
    Task<bool> RegisterHistoryTask(HistoricoDaTarefa taskHistory);
    Task<bool> AddComment(Comentario comment);
}