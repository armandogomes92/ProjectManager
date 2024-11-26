using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Project.Commands;

public class AddProjectCommand : Command
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int UserId { get; set; }
}