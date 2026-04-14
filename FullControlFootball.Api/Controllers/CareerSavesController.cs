using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.CareerSaves.Contracts;
using FullControlFootball.Application.Features.CareerSaves.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullControlFootball.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/career-saves")]
public sealed class CareerSavesController : ControllerBase
{
    private readonly ICareerSaveService _careerSaveService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public CareerSavesController(ICareerSaveService careerSaveService, ICurrentUserAccessor currentUserAccessor)
    {
        _careerSaveService = careerSaveService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CareerSaveResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCareerSaveRequest request, CancellationToken cancellationToken)
    {
        var response = await _careerSaveService.CreateAsync(_currentUserAccessor.GetRequiredUserId(), request, cancellationToken);
        return Ok(response);
    }
}
