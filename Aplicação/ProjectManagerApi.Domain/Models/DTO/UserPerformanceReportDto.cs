namespace ProjectManagerApi.Domain.Models.DTO;

public class UserPerformanceReportDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public double AverageTasksCompleted { get; set; }
}