using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.Rankings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullControlFootball.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/top-assists")]
public sealed class TopAssistsController : ControllerBase
{
    private readonly IRankingQueryService _rankingQueryService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public TopAssistsController(IRankingQueryService rankingQueryService, ICurrentUserAccessor currentUserAccessor)
    {
        _rankingQueryService = rankingQueryService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpGet("{seasonCompetitionId:guid}")]
    public async Task<IActionResult> GetByCompetition(Guid seasonCompetitionId, CancellationToken cancellationToken)
    {
        var items = await _rankingQueryService.GetTopAssistsAsync(_currentUserAccessor.GetRequiredUserId(), seasonCompetitionId, cancellationToken);
        return Ok(items);
    }
}
