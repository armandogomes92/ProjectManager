using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Enums;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;

public class AddTasksByProjectIdCommand : Command
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime PrazoConclusao { get; set; }
    public PriorityEnum Prioridade { get; set; }
    public int ProjetoId { get; set; }
}