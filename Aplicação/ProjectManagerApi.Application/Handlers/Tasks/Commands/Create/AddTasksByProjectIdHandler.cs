using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Enums;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;

public class AddTasksByProjectIdHandler : CommandHandler<AddTasksByProjectIdCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public AddTasksByProjectIdHandler(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    public override async Task<Response> Handle(AddTasksByProjectIdCommand command, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetProjectById(command.ProjetoId);
        if (project == null)
        {
            return new Response { Content = Messages.ProjectNotFound };
        }
        if (!await _taskRepository.CanBeAddTask(command.ProjetoId))
        {
            return new Response { Content = Messages.TaskNotCanBeAdded };
        }
        else
        {
            var task = new Domain.Models.DataModels.Tarefa
            {
                Titulo = command.Titulo,
                Descricao = command.Descricao,
                PrazoConclusao = command.PrazoConclusao,
                UltimaAtualizacao = DateTime.Now,
                Status = StatusEnum.Pendente,
                Prioridade = command.Prioridade,
                ProjetoId = command.ProjetoId
            };
            _taskRepository.CreateTask(task);

            return new Response { Content = Messages.TaskCreated };
        }
    }
}