using MediatR;
using PersonalAssistantApi.Application.Common;

namespace PersonalAssistantApi.Application.Features.Tarefas.ConcluirTarefa;

public record ConcluirTarefaCommand(Guid TarefaId, Guid UsuarioId) : IRequest<Result<bool>>;
