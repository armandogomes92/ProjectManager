using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Report.Queries;

public class GetReportsFromTheLast30DaysCommand : Command
{
    public int UserId { get; set; }
}