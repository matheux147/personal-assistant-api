using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using PersonalAssistantApi.Services.SemanticKernel.Interfaces;

namespace PersonalAssistantApi.Services.SemanticKernel;

public class AiService(Kernel kernel) : IAiService
{
    private readonly Kernel _kernel = kernel;

    public async Task<object> ProcessarSolicitacao(Guid usuarioId, string inputUsuario)
    {
        var chatCompletionService =
            _kernel.GetRequiredService<IChatCompletionService>();

        var settings = new OpenAIPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(autoInvoke: false)
        };

        var history = new ChatHistory();

        history.AddSystemMessage($@"
            Você é um assistente pessoal integrado a uma interface visual interativa.
            Data atual: {DateTime.Now:yyyy-MM-dd}. 
            ID do Usuário: {usuarioId}.

            REGRAS DE COMPORTAMENTO:
            1. Se o usuário quiser CONCLUIR uma tarefa ou PAGAR uma conta, NÃO faça perguntas. Chame IMEDIATAMENTE as funções de busca de pendências.
            2. A interface gráfica cuidará da seleção do item.
            
            REGRAS DE DADOS:
            1. Tarefas: Título APENAS com o nome da ação. SEM data/hora no texto.
            2. Contas: Descrição APENAS com o nome. SEM valor no texto.
            3. Agendamentos (Consultas, Reuniões) são TAREFAS.
            Atenção: Qualquer agendamento, por exemplo: Consultas, Reuniões, Eventos, etc., deve ser registrado como TAREFA.
            ");

        history.AddUserMessage(inputUsuario);

        var result = await chatCompletionService
            .GetChatMessageContentAsync(history, settings, _kernel);

        var calls = FunctionCallContent.GetFunctionCalls(result);
        if (calls != null && calls.Any())
        {
            var call = calls.First();
            var funcResult = await call.InvokeAsync(_kernel);
            return funcResult.Result!;
        }

        return result.Content ?? "Não entendi a solicitação.";
    }
}