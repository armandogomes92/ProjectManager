using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Update;

public class UpdateTaskByIdHandler : CommandHandler<UpdateTaskByIdCommand>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskByIdHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Response> Handle(UpdateTaskByIdCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskById(command.Id);
        if (task == null)
        {
            return new Response { Content = Messages.TaskNotFound };
        }

        var taskHistory = new HistoricoDaTarefa
        {
            TaskId = task.Id,
            ItemModificado = "Task",
            ValoresAntigos = $@"Titulo : {task.Titulo}, descrição: {task.Descricao}, status: {task.Status}, prazo: {task.PrazoConclusao}",
            ValoresNovos = $@"Titulo : {command.Title}, descrição: {command.Description}, status: {command.Status}, prazo: {command.NewDeadLine}",
            DataModificacao = DateTime.Now
        };

        task.Titulo = command.Title;
        task.Descricao = command.Description;
        task.Status = command.Status;
        task.PrazoConclusao = command.NewDeadLine;
        task.UltimaAtualizacao = DateTime.Now;
        if (await _taskRepository.UpdateTaskById(task))
        {
            await _taskRepository.RegisterHistoryTask(taskHistory);
        }
        return new Response { Content = Messages.TaskUpdated };
    }
}
