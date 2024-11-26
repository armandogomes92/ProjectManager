using ProjectManagerApi.Domain.Models.DataModels;

namespace ProjectManagerApi.Infrastructure.Contracts;

public interface IProjectRepository
{
    Task<IEnumerable<Projeto>> GetAllProjects(int userId);
    Task<Projeto> GetProjectById(int projectId);
    bool CreateProject(Projeto projeto);
    bool DisableProject(Projeto projeto);
}