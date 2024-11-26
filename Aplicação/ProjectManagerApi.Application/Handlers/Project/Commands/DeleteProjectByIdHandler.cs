using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Project.Commands;

public class DeleteProjectByIdHandler : CommandHandler<DeleteProjectByIdCommand>
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITaskRepository _taskRepository;

    public DeleteProjectByIdHandler(IProjectRepository projectRepository, ITaskRepository taskRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
    }

    public override async Task<Response> Handle(DeleteProjectByIdCommand command, CancellationToken cancellationToken)
    {
        if (await _taskRepository.CheckIfPendingTasksByProjectId(command.ProjectId))
        {
            return new Response { Content = Messages.ProjectHasPendingTasks };
        }
        var project = await _projectRepository.GetProjectById(command.ProjectId);

        if (project == null)
        {
            return new Response { Content = Messages.ProjectNotFound };
        }
        return new Response { Content = Messages.ProjectDisabled };
    }
}