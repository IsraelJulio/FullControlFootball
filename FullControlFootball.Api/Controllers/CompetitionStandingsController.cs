using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.CompetitionStandings.Contracts;
using FullControlFootball.Application.Features.CompetitionStandings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullControlFootball.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/competition-standings")]
public sealed class CompetitionStandingsController : ControllerBase
{
    private readonly ICompetitionStandingService _service;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public CompetitionStandingsController(ICompetitionStandingService service, ICurrentUserAccessor currentUserAccessor)
    {
        _service = service;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompetitionStandingResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCompetitionStandingRequest request, CancellationToken cancellationToken)
    {
        var response = await _service.CreateAsync(_currentUserAccessor.GetRequiredUserId(), request, cancellationToken);
        return Ok(response);
    }
}
