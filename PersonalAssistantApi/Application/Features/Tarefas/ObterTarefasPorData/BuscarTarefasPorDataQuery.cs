using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;

namespace PersonalAssistantApi.Application.Features.Tarefas.ObterTarefasPorData;

public record BuscarTarefasPorDataQuery(Guid UsuarioId, DateTime DataInicio, DateTime DataFim)
    : IRequest<Result<IEnumerable<TarefaDto>>>;
