using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Tarefas.ObterTarefasPendentes.Handler_;

public class BuscarTarefasPendentesHandler(ITarefaRepository repository)
    : IRequestHandler<BuscarTarefasPendentesQuery, Result<IEnumerable<TarefaDto>>>
{
    public async Task<Result<IEnumerable<TarefaDto>>> Handle(BuscarTarefasPendentesQuery request, CancellationToken cancellationToken)
    {
        var tarefas = await repository.GetAllPendentesByUsuarioIdAsync(request.UsuarioId);

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