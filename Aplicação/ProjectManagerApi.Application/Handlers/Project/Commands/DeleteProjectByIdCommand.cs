using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Project.Commands;

public class DeleteProjectByIdCommand : Command
{
    public int ProjectId { get; set; }
}