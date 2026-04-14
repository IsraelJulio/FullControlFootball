using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.Rankings.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullControlFootball.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/top-scorers")]
public sealed class TopScorersController : ControllerBase
{
    private readonly IRankingQueryService _rankingQueryService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public TopScorersController(IRankingQueryService rankingQueryService, ICurrentUserAccessor currentUserAccessor)
    {
        _rankingQueryService = rankingQueryService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpGet("{seasonCompetitionId:guid}")]
    public async Task<IActionResult> GetByCompetition(Guid seasonCompetitionId, CancellationToken cancellationToken)
    {
        var items = await _rankingQueryService.GetTopScorersAsync(_currentUserAccessor.GetRequiredUserId(), seasonCompetitionId, cancellationToken);
        return Ok(items);
    }
}
