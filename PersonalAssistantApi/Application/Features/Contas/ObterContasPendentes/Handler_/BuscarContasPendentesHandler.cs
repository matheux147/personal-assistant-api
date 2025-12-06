using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Contas.ObterContasPendentes;

public class BuscarContasPendentesHandler(IContaRepository repository)
    : IRequestHandler<BuscarContasPendentesQuery, Result<IEnumerable<ContaDto>>>
{
    public async Task<Result<IEnumerable<ContaDto>>> Handle(BuscarContasPendentesQuery request, CancellationToken cancellationToken)
    {
        var contas = await repository.GetPendentesByUsuarioIdAsync(request.UsuarioId);

        var dtos = contas.Select(c => new ContaDto
        {
            Id = c.Id,
            Descricao = c.Descricao,
            Valor = c.Valor,
            DataVencimento = c.DataVencimento,
            Pago = c.Pago
        });

        return Result<IEnumerable<ContaDto>>.Success(dtos);
    }
}
