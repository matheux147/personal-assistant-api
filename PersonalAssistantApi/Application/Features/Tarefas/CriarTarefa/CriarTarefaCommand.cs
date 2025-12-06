using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;

namespace PersonalAssistantApi.Application.Features.Tarefas.CriarTarefa;

public record CriarTarefaCommand(CriarTarefaDto Dto) : IRequest<Result<Guid>>;