using ProjectManagerApi.Domain.Models.DTO;

namespace ProjectManagerApi.Infrastructure.Contracts;

public interface IReportRepository
{
    Task<IEnumerable<UserPerformanceReportDto>> GetUserPerformanceReportAsync();
}
