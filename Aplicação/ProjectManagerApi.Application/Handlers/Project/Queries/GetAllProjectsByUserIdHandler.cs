using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Project.Queries;

public class GetAllProjectsByUserIdHandler : QueryHandler<GetAllProjectsByUserIdQuery>
{
    private readonly IProjectRepository _projectRepository;

    public GetAllProjectsByUserIdHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public override async Task<Response> Handle(GetAllProjectsByUserIdQuery query, CancellationToken cancellationToken)
    {
        return new Response { Content = await _projectRepository.GetAllProjects(query.UserId) };
    }
}