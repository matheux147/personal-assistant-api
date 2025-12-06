using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;

namespace PersonalAssistantApi.Application.Features.Contas.ObterContas;

public record BuscarContasQuery(Guid UsuarioId) :
    IRequest<Result<IEnumerable<ContaDto>>>;
