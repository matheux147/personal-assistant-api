using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;

namespace PersonalAssistantApi.Application.Features.Tarefas.ObterTarefasPendentes;

public record BuscarTarefasPendentesQuery(Guid UsuarioId) : IRequest<Result<IEnumerable<TarefaDto>>>;
