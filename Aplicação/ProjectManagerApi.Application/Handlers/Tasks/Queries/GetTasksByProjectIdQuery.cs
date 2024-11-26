using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Tasks.Queries;

public class GetTasksByProjectIdQuery : Query
{
    public int ProjectId { get; set; }
}