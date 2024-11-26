using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerApi.Application.Handlers.Project.Commands;
using ProjectManagerApi.Application.Handlers.Project.Queries;
using ProjectManagerApi.Domain.Models.CommonResources;

namespace ProjectManagerApi.Controllers;

[ApiController]
[Route("projeto")]
public class ProjectController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll([FromQuery] int userId)
    {
        return Ok(await _mediator.Send(new GetAllProjectsByUserIdQuery { UserId = userId}));
    }

    [HttpPost]
    public async Task<IActionResult> AddProject([FromBody] AddProjectCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProject([FromBody] DeleteProjectByIdCommand command)
    {
        var response = await _mediator.Send(command);
        if ((string)response.Content! == Messages.ProjectDisabled)
        {
            return Ok(response.Content);
        }
        else if ((string)response.Content! == Messages.ProjectHasPendingTasks)
        {
            return BadRequest(response);
        }
        else
        {
            return NotFound(response);
        }
    }
}