using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Tarefas.ObterTarefas;

public class BuscarTarefasHandler : IRequestHandler<BuscarTarefasQuery, Result<IEnumerable<TarefaDto>>>
{
    private readonly ITarefaRepository _repository;

    public BuscarTarefasHandler(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<TarefaDto>>> Handle(BuscarTarefasQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await _repository.GetAllByUsuarioIdAsync(request.UsuarioId);

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
