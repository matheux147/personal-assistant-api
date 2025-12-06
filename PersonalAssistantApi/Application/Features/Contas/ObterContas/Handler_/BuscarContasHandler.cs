using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Contas.ObterContas;

public class BuscarContasHandler(IContaRepository repository) : IRequestHandler<BuscarContasQuery, Result<IEnumerable<ContaDto>>>
{
    private readonly IContaRepository _repository = repository;

    public async Task<Result<IEnumerable<ContaDto>>> Handle(BuscarContasQuery request, CancellationToken cancellationToken)
    {
        var contas = await _repository.GetAllByUsuarioIdAsync(request.UsuarioId);

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
