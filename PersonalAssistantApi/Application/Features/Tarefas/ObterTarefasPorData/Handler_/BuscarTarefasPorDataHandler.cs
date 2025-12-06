using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Tarefas.ObterTarefasPorData;

public class BuscarTarefasPorDataHandler : IRequestHandler<BuscarTarefasPorDataQuery, Result<IEnumerable<TarefaDto>>>
{
    private readonly ITarefaRepository _repository;

    public BuscarTarefasPorDataHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<TarefaDto>>> Handle(BuscarTarefasPorDataQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await _repository.GetByRangeAsync(request.UsuarioId, request.DataInicio, request.DataFim);

        var dtos = tarefas.Select(t => new TarefaDto
        {
            Id = t.Id,
            Titulo = t.Titulo,
            Data = t.Data,
            Concluida = t.Concluida
        });
        return Result<IEnumerable<TarefaDto>>.Success(dtos);
    }
}
