using MediatR;
using Microsoft.SemanticKernel;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.DTOs.Contas;
using PersonalAssistantApi.Application.DTOs.Tarefas;
using PersonalAssistantApi.Application.Features.Contas.CriarConta;
using PersonalAssistantApi.Application.Features.Contas.ObterContas;
using PersonalAssistantApi.Application.Features.Contas.ObterContasPendentes;
using PersonalAssistantApi.Application.Features.Contas.ObterContasPorData;
using System.ComponentModel;

namespace PersonalAssistantApi.Services.SemanticKernel.Functions;

public class ContasFunctions(IMediator mediator)
{
    private readonly IMediator _mediator = mediator;

    [KernelFunction, Description("Busca as contas a pagar dentro de um intervalo de datas. Se datas forem fornecidas, filtra por período. Caso contrário, traz todas.")]
    public async Task<object> BuscarContas(
        [Description("ID do usuário (GUID)")] string usuarioId,
        [Description("Data Inicial (opcional) yyyy-MM-dd HH:mm")] string? dataInicio = null,
        [Description("Data Final (opcional) yyyy-MM-dd HH:mm")] string? dataFim = null,
        [Description("Se 'true', retorna apenas contas NÃO pagas (pendentes). Use isso quando o usuário perguntar 'o que tenho para pagar' em um período específico.")]
        bool? somentePendentes = null)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return Result<IEnumerable<ContaDto>>.Failure("ID de usuário inválido.");

        if (!string.IsNullOrEmpty(dataInicio) && !string.IsNullOrEmpty(dataFim))
        {
            if (DateTime.TryParse(dataInicio, out var inicio) && DateTime.TryParse(dataFim, out var fim))
            {
                return await _mediator.Send(new BuscarContasPorDataQuery(uid, inicio, fim, somentePendentes));
            }
            return Result<IEnumerable<TarefaDto>>.Failure("Datas inválidas.");
        }

        return await _mediator.Send(new BuscarContasQuery(uid));
    }

    [KernelFunction, Description("Registra uma nova conta a pagar (boleto, fatura, etc).")]
    public async Task<object> CriarConta(
        [Description("ID do usuário")] string usuarioId,
        [Description("Descrição da conta (ex: 'Internet'). NÃO inclua o valor ou vencimento neste texto.")] string descricao,
        [Description("Valor monetário (ex: 150.50)")] double valor,
        [Description("Data de Vencimento yyyy-MM-dd")] string dataVencimento)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return Result<Guid>.Failure("ID inválido.");

        if (!DateTime.TryParse(dataVencimento, out var data))
            return Result<Guid>.Failure("Data inválida.");

        if (valor <= 0)
            return Result<Guid>.Failure("O valor deve ser maior que zero.");

        var dto = new CriarContaDto
        {
            UsuarioId = uid,
            Descricao = descricao,
            Valor = (decimal)valor,
            DataVencimento = data
        };

        return await _mediator.Send(new CriarContaCommand(dto));
    }

    [KernelFunction, Description(@"
        Retorna a lista de contas pendentes não pagas.
        ATENÇÃO: Use esta função IMEDIATAMENTE sempre que o usuário expressar intenção de pagar, quitar ou dar baixa em uma conta.
        NÃO pergunte qual conta é. Apenas chame esta função para mostrar a lista.")]
    public async Task<object> BuscarContasPendentes(
    [Description("ID do usuário")] string usuarioId)
    {
        if (!Guid.TryParse(usuarioId, out var uid))
            return Result<IEnumerable<ContaDto>>.Failure("ID inválido.");

        return await _mediator.Send(new BuscarContasPendentesQuery(uid));
    }
}
