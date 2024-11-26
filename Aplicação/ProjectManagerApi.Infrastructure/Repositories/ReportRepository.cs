using Microsoft.EntityFrameworkCore;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.DTO;
using ProjectManagerApi.Infrastructure.Contracts;
using ProjectManagerApi.Infrastructure.DataContextConfigurations;

namespace ProjectManagerApi.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;
    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<UserPerformanceReportDto>> GetUserPerformanceReportAsync()
    {
        var thirtyDaysAgo = DateTime.Now.AddDays(-30);

        var userPerformance = await _context.Users
            .Select(user => new UserPerformanceReportDto
            {
                UserId = user.Id,
                UserName = user.Nome,
                AverageTasksCompleted = _context.Tasks
                    .Where(task => task.Projeto.UserId == user.Id && task.Status == StatusEnum.Concluída && task.UltimaAtualizacao.Date >= thirtyDaysAgo.Date)
                    .Count() / 30.0
            })
            .ToListAsync();
        return userPerformance;
    }
}