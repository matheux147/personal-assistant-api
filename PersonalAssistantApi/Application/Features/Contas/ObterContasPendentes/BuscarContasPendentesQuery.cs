using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;

namespace PersonalAssistantApi.Application.Features.Contas.ObterContasPendentes;

public record BuscarContasPendentesQuery(Guid UsuarioId) : IRequest<Result<IEnumerable<ContaDto>>>;
