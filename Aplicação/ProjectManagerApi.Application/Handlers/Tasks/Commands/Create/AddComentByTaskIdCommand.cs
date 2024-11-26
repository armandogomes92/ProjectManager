using ProjectManagerApi.Application.Handlers.CommonResources;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;

public class AddComentByTaskIdCommand : Command
{
    public string TextOfComment { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
}