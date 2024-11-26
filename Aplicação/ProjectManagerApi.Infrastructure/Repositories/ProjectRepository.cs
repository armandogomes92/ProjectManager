using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;

namespace ProjectManagerApi.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;
    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Projeto>> GetAllProjects(int userId)
    {
        return await _context.Projetos.Where(x => x.UserId == userId && x.Active).ToListAsync();
    }

    public async Task<Projeto> GetProjectById(int projectId)
    {
        return await _context.Projetos.FirstOrDefaultAsync(x => x.Id == projectId);
    }

    public bool CreateProject(Projeto projeto)
    {
        _context.Projetos.Add(projeto);

        return _context.SaveChanges() > 0;
    }

    public bool DisableProject(Projeto projeto)
    {
        _context.Projetos.Update(projeto);

        return _context.SaveChanges() > 0;
    }
}