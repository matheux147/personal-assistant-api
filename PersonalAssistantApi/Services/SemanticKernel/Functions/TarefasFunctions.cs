using MediatR;
using Microsoft.SemanticKernel;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Tarefas;
using PersonalAssistantApi.Application.Features.Tarefas.CriarTarefa;
using PersonalAssistantApi.Application.Features.Tarefas.ObterTarefas;
using PersonalAssistantApi.Application.Features.Tarefas.ObterTarefasPendentes;
using PersonalAssistantApi.Application.Features.Tarefas.ObterTarefasPorData;
using System.ComponentModel;

namespace PersonalAssistantApi.Services.SemanticKernel.Functions;

public class TarefasFunctions(IMediator mediator)
{
    private readonly IMediator _mediator = mediator;

    [KernelFunction, Description("Cria uma nova tarefa.")]
    public async Task<object> CriarTarefa(
        [Description("ID do usuário")] string usuarioId,
        [Description("Título da tarefa. Apenas o nome da ação (ex: 'Estudar'). NÃO inclua data ou hora neste texto.")] string titulo,
        [Description("Data yyyy-MM-dd HH:mm")] string data)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return Result<Guid>.Failure("ID de usuário inválido.");

        if (!DateTime.TryParse(data, out var dataTarefa))
            return Result<Guid>.Failure("Data inválida.");

        var dto = new CriarTarefaDto { UsuarioId = uid, Titulo = titulo, Data = dataTarefa };

        return await _mediator.Send(new CriarTarefaCommand(dto));
    }

    [KernelFunction, Description("Busca tarefas. Se datas forem fornecidas, filtra por período. Caso contrário, traz todas.")]
    public async Task<object> BuscarTarefas(
        [Description("ID do usuário")] string usuarioId,
        [Description("Data Inicial (opcional) yyyy-MM-dd HH:mm")] string? dataInicio = null,
        [Description("Data Final (opcional) yyyy-MM-dd HH:mm")] string? dataFim = null)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return Result<IEnumerable<TarefaDto>>.Failure("ID inválido.");

        if (!string.IsNullOrEmpty(dataInicio) && !string.IsNullOrEmpty(dataFim))
        {
            if (DateTime.TryParse(dataInicio, out var inicio) && DateTime.TryParse(dataFim, out var fim))
            {
                return await _mediator.Send(new BuscarTarefasPorDataQuery(uid, inicio, fim));
            }
            return Result<IEnumerable<TarefaDto>>.Failure("Datas inválidas.");
        }

        return await _mediator.Send(new BuscarTarefasQuery(uid));
    }

    [KernelFunction, Description(@"
        Retorna a lista de tarefas pendentes não concluidas. ")]
    public async Task<object> BuscarTarefasPendentes(
    [Description("ID do usuário")] string usuarioId)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return Result<IEnumerable<TarefaDto>>.Failure("ID inválido.");

        return await _mediator.Send(new BuscarTarefasPendentesQuery(uid));
    }
}
