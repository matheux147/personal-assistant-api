using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalAssistantApi.Application.Common.Interfaces;
using PersonalAssistantApi.Application.Features.Tarefas.ConcluirTarefa;
using PersonalAssistantApi.Application.Features.Tarefas.ObterTarefas;

namespace PersonalAssistantApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApiResponseHandler _responseHandler;

    public TarefasController(IMediator mediator, IApiResponseHandler responseHandler)
    {
        _mediator = mediator;
        _responseHandler = responseHandler;
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorResponseDto), StatusCodes.Status500InternalServerError)]
    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> ObterTarefas([FromRoute] string usuarioId)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return BadRequest("ID de usuário inválido.");

        var result = await _mediator.Send(new BuscarTarefasQuery(uid));

        return _responseHandler.Handle(result);
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorResponseDto), StatusCodes.Status500InternalServerError)]
    [HttpPatch("{id}/concluir")]
    public async Task<IActionResult> Concluir(Guid id, [FromBody] ActionRequestDto request)
    {
        if (!Guid.TryParse(request.UsuarioId, out var uid))
            return BadRequest("ID de usuário inválido.");

        var result = await _mediator.Send(new ConcluirTarefaCommand(id, uid));

        return _responseHandler.Handle(result);
    }
}
