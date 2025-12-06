using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Domain.Entities;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Contas.CriarConta;

public class CriarContaHandler(IContaRepository repository)
    : IRequestHandler<CriarContaCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CriarContaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var conta = new Conta(
                request.Dto.UsuarioId,
                request.Dto.Descricao,
                request.Dto.Valor,
                request.Dto.DataVencimento
            );

            await repository.AddAsync(conta);
            return Result<Guid>.Success(conta.Id);
        }
        catch (ArgumentException ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
    }
}
