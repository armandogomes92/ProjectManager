using ProjectManagerApi.Application.Handlers.CommonResources;
using ProjectManagerApi.Domain.Models.CommonResources;
using ProjectManagerApi.Domain.Models.DataModels;
using ProjectManagerApi.Infrastructure.Contracts;

namespace ProjectManagerApi.Application.Handlers.Project.Commands;

public class AddProjectHandler : CommandHandler<AddProjectCommand>
{
    private readonly IProjectRepository _projectRepository;

    public AddProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public override async Task<Response> Handle(AddProjectCommand command, CancellationToken cancellationToken)
    {
        var project = new Projeto
        {
            Id = command.Id,
            Nome = command.Nome,
            Active = true,
            UserId = command.UserId
        };

        _projectRepository.CreateProject(project);
        return new Response();
    }
}