using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Contas.PagarConta;

public class PagarContaHandler(IContaRepository repository) : IRequestHandler<PagarContaCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(PagarContaCommand request, CancellationToken cancellationToken)
    {
        var conta = await repository.GetByIdAsync(request.ContaId);

        if (conta == null) return Result<bool>.Failure("Conta não encontrada.");
        if (conta.UsuarioId != request.UsuarioId) return Result<bool>.Failure("Conta não pertence ao usuário.");

        conta.Pagar();
        await repository.UpdateAsync(conta);

        return Result<bool>.Success(true);
    }
}
