using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Contas.ObterContasPorData;

public class BuscarContasPorDataHandler(IContaRepository repository) : IRequestHandler<BuscarContasPorDataQuery, Result<IEnumerable<ContaDto>>>
{
    private readonly IContaRepository _repository = repository;

    public async Task<Result<IEnumerable<ContaDto>>> Handle(BuscarContasPorDataQuery request, CancellationToken cancellationToken)
    {
        var contas = await _repository.GetByVencimentoAsync(request.UsuarioId, request.Inicio, request.Fim, request.SomentePendentes);

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
