using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjectManagerApi.Application.Handlers.Report.Queries;

namespace ProjectManagerApi.Controllers;

[Route("relatorio")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetReportsFromTheLast30Days([FromQuery] int userId)
    {
        return Ok(await _mediator.Send(new GetReportsFromTheLast30DaysCommand { UserId = userId }));
    }
}