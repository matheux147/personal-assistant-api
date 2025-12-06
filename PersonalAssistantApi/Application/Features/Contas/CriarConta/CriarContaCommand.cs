using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;

namespace PersonalAssistantApi.Application.Features.Contas.CriarConta;

public record CriarContaCommand(CriarContaDto Dto) : IRequest<Result<Guid>>;