using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.Seasons.Contracts;
using FullControlFootball.Application.Features.Seasons.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullControlFootball.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/seasons")]
public sealed class SeasonsController : ControllerBase
{
    private readonly ISeasonService _seasonService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public SeasonsController(ISeasonService seasonService, ICurrentUserAccessor currentUserAccessor)
    {
        _seasonService = seasonService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpPost]
    [ProducesResponseType(typeof(SeasonResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateSeasonRequest request, CancellationToken cancellationToken)
    {
        var response = await _seasonService.CreateAsync(_currentUserAccessor.GetRequiredUserId(), request, cancellationToken);
        return Ok(response);
    }
}
