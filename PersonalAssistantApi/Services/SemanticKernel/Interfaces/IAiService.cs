namespace PersonalAssistantApi.Services.SemanticKernel.Interfaces;

public interface IAiService
{
    Task<object> ProcessarSolicitacao(Guid usuarioId, string inputUsuario);
}
