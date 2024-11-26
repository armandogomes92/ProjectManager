using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Delete;

public class DeleteTaskByIdCommand : Command
{
    public int Id { get; set; }
}
