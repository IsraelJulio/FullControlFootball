using FullControlFootball.Application.Common.Security;
using FullControlFootball.Application.Features.Transfers.Contracts;
using FullControlFootball.Application.Features.Transfers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullControlFootball.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/transfers")]
public sealed class TransfersController : ControllerBase
{
    private readonly ITransferService _transferService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public TransfersController(ITransferService transferService, ICurrentUserAccessor currentUserAccessor)
    {
        _transferService = transferService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpPost]
    [ProducesResponseType(typeof(TransferTransactionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateTransferTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _transferService.CreateAsync(_currentUserAccessor.GetRequiredUserId(), request, cancellationToken);
        return Ok(response);
    }
}
