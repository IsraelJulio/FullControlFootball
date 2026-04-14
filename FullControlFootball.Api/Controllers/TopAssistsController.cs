using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.Rankings.Contracts;
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
    private readonly IRankingCommandService _rankingCommandService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public TopAssistsController(
        IRankingQueryService rankingQueryService,
        IRankingCommandService rankingCommandService,
        ICurrentUserAccessor currentUserAccessor)
    {
        _rankingQueryService = rankingQueryService;
        _rankingCommandService = rankingCommandService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpGet("{seasonCompetitionId:guid}")]
    public async Task<IActionResult> GetByCompetition(Guid seasonCompetitionId, CancellationToken cancellationToken)
    {
        var items = await _rankingQueryService.GetTopAssistsAsync(_currentUserAccessor.GetRequiredUserId(), seasonCompetitionId, cancellationToken);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompetitionTopAssistsRequest request, CancellationToken cancellationToken)
    {
        var items = await _rankingCommandService.CreateTopAssistsAsync(_currentUserAccessor.GetRequiredUserId(), request, cancellationToken);
        return Ok(items);
    }
}
