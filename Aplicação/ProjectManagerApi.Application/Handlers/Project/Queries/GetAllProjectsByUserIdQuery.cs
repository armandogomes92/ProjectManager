using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Project.Queries;

public class GetAllProjectsByUserIdQuery : Query
{
    public int UserId { get; set; }
}
