using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Enums;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Update;

public class UpdateTaskByIdCommand : Command
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime NewDeadLine { get; set; }
}