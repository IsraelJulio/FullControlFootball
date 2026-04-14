using FullControlFootball.Application.Features.Transfers.Contracts;

namespace FullControlFootball.Application.Features.Transfers.Services;

public interface ITransferService
{
    Task<TransferTransactionResponse> CreateAsync(Guid userId, CreateTransferTransactionRequest request, CancellationToken cancellationToken);
}
