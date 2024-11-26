using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Tasks.Queries;

public class GetTasksByProjectIdHandler : QueryHandler<GetTasksByProjectIdQuery>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksByProjectIdHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Response> Handle(GetTasksByProjectIdQuery query, CancellationToken cancellationToken)
    {
        return new Response { Content = await _taskRepository.GetAllTasksByProjectId(query.ProjectId) };
    }
}