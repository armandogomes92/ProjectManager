using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;

namespace ProjectManagerApi.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Domain.Models.DataModels.Tarefa>> GetAllTasksByProjectId(int projetoId)
    {
        return await _context.Tasks.Where(x => x.ProjetoId == projetoId && x.Projeto.Active).Include(x => x.comentarios).ToListAsync();
    }
    public async Task<Domain.Models.DataModels.Tarefa> GetTaskById(int taskId)
    {
        return await _context.Tasks.Where(s => s.Id == taskId).Include(x => x.comentarios).FirstOrDefaultAsync();
    }
    public bool CreateTask(Domain.Models.DataModels.Tarefa task)
    {
        _context.Tasks.Add(task);
        return _context.SaveChanges() > 0;
    }
    public async Task<bool> UpdateTaskById(Domain.Models.DataModels.Tarefa task)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync() > 0;
    }
    public bool DeleteTaskById(Domain.Models.DataModels.Tarefa task)
    {
        _context.Tasks.Remove(task);
        return _context.SaveChanges() > 0;
    }
    public async Task<bool> CheckIfPendingTasksByProjectId(int projectId)
    {
        return await _context.Tasks.AnyAsync(x => x.ProjetoId == projectId && x.Status == StatusEnum.Pendente);
    }
    public async Task<bool> CanBeAddTask(int projectId)
    {
        var result = await _context.Tasks.Where(x => x.ProjetoId == projectId).ToListAsync();
        return result.Count <= 20;
    }
    public async Task<bool> RegisterHistoryTask(HistoricoDaTarefa taskHistory)
    {
        _context.TaskHistories.Update(taskHistory);
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> AddComment(Comentario comment)
    {
        _context.Comments.Add(comment);
        return await _context.SaveChangesAsync() > 0;
    }
}