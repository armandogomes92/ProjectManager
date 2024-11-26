using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Tasks.Commands.Delete;

public class DeleteTaskByIdHandler : CommandHandler<DeleteTaskByIdCommand>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskByIdHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public override async Task<Response> Handle(DeleteTaskByIdCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskById(command.Id);
        if (task == null)
        {
            return new Response { Content = Messages.TaskNotFound };
        }

        _taskRepository.DeleteTaskById(task);
        return new Response { Content = Messages.TaskDeleted };
    }
}
