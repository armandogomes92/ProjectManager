using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;

public class AddComentByTaskIdHandler : CommandHandler<AddComentByTaskIdCommand>
{
    private readonly ITaskRepository _taskRepository;

    public AddComentByTaskIdHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Response> Handle(AddComentByTaskIdCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskById(command.TaskId);

        if (task == null)
        {
            return new Response { Content = Messages.TaskNotFound };
        }
        var comment = new Comentario
        {
            TextoComentario = command.TextOfComment,
            TarefaId = command.TaskId,
            UsuarioId = command.UserId
        };

        if (await _taskRepository.AddComment(comment))
        {
            var taskHistory = new HistoricoDaTarefa
            {
                TaskId = command.TaskId,
                ItemModificado = "Comment",
                ValoresAntigos = "",
                ValoresNovos = command.TextOfComment,
                DataModificacao = DateTime.Now
            };

            await _taskRepository.RegisterHistoryTask(taskHistory);
        }
        return new Response { Content = Messages.CommentAdded };
    }
}
