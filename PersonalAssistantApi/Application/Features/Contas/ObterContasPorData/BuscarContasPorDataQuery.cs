using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;

namespace PersonalAssistantApi.Application.Features.Contas.ObterContasPorData;

public record BuscarContasPorDataQuery(Guid UsuarioId, DateTime Inicio, DateTime Fim, bool? SomentePendentes = null) : 
    IRequest<Result<IEnumerable<ContaDto>>>;
