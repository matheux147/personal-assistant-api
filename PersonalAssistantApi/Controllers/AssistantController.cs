using Microsoft.AspNetCore.Mvc;
using PersonalAssistantApi.Application.Common.Interfaces;
using PersonalAssistantApi.Domain.Repositories;
using PersonalAssistantApi.Services.SemanticKernel.Interfaces;

namespace PersonalAssistantApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssistantController : ControllerBase
{
    private readonly IAiService _aiService;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IApiResponseHandler _responseHandler;

    public AssistantController(
        IAiService aiService,
        IUsuarioRepository usuarioRepository,
        IApiResponseHandler responseHandler)
    {
        _aiService = aiService;
        _usuarioRepository = usuarioRepository;
        _responseHandler = responseHandler;
    }

    [ProducesResponseType(typeof(AiResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreatedResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(BadRequestResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorResponseDto), StatusCodes.Status500InternalServerError)]
    [HttpPost("ask")]
    public async Task<IActionResult> Ask([FromBody] AskRequestDto request)
    {
        if (!Guid.TryParse(request.UsuarioId, out var uid))
            return BadRequest("ID inválido.");

        var usuario = await _usuarioRepository.GetByIdAsync(uid);
        if (usuario == null) return NotFound("Usuário não encontrado.");

        var resultObject = await _aiService.ProcessarSolicitacao(uid, request.Pedido);

        return _responseHandler.Handle(resultObject);
    }
}