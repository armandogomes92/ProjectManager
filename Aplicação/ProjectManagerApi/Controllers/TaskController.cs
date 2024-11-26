using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Create;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Delete;
using ProjectManagerApi.Application.Handlers.Tasks.Commands.Update;
using ProjectManagerApi.Application.Handlers.Tasks.Queries;
using ProjectManagerApi.Domain.Models.CommonResources;

namespace ProjectManagerApi.Controllers;

[Route("tarefa")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediator;

    public TaskController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{projectId")]
    public async Task<IActionResult> GetByProjectId([FromQuery] int projectId)
    {
        return Ok(await _mediator.Send(new GetTasksByProjectIdQuery { ProjectId = projectId}));
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddTasksByProjectIdCommand command)
    {
        var response = await _mediator.Send(command);
        if ((string)response.Content! == Messages.ProjectNotFound || (string)response.Content! == Messages.TaskNotCanBeAdded)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(UpdateTaskByIdCommand command)
    {
        var response = await _mediator.Send(command);
        if ((string)response.Content! == Messages.ProjectNotFound)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteTaskByIdCommand command)
    {
        var response = await _mediator.Send(command);
        if ((string)response.Content! == Messages.ProjectNotFound)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
    [HttpPost("comentario")]
    public async Task<IActionResult> Post(AddComentByTaskIdCommand command)
    {
        var response = await _mediator.Send(command);
        if ((string)response.Content! == Messages.TaskNotFound)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}