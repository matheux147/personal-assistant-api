using MediatR;
using PersonalAssistantApi.Application.Common;

namespace PersonalAssistantApi.Application.Features.Contas.PagarConta;

public record PagarContaCommand(Guid ContaId, Guid UsuarioId) : IRequest<Result<bool>>;
