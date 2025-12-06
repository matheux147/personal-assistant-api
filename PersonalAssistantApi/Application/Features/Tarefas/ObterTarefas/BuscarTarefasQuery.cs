using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;

namespace PersonalAssistantApi.Application.Features.Tarefas.ObterTarefas;

public record BuscarTarefasQuery(Guid UsuarioId) : IRequest<Result<IEnumerable<TarefaDto>>>;
