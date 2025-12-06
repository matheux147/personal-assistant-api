using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonalAssistantApi.Application.Common.Interfaces;
using PersonalAssistantApi.Application.Features.Contas.ObterContas;
using PersonalAssistantApi.Application.Features.Contas.PagarConta;

namespace PersonalAssistantApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IApiResponseHandler _responseHandler;

    public ContasController(IMediator mediator, IApiResponseHandler responseHandler)
    {
        _mediator = mediator;
        _responseHandler = responseHandler;
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorResponseDto), StatusCodes.Status500InternalServerError)]
    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> ObterContas([FromRoute] string usuarioId)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return BadRequest("ID de usuário inválido.");

        var result = await _mediator.Send(new BuscarContasQuery(uid));

        return _responseHandler.Handle(result);
    }

    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponseDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponseDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorResponseDto), StatusCodes.Status500InternalServerError)]
    [HttpPatch("{id}/pagar")]
    public async Task<IActionResult> Pagar(Guid id, [FromBody] ActionRequestDto request)
    {
        if (!Guid.TryParse(request.UsuarioId, out var uid))
            return BadRequest("ID de usuário inválido.");

        var result = await _mediator.Send(new PagarContaCommand(id, uid));

        return _responseHandler.Handle(result);
    }
}
