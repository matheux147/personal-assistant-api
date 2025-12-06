namespace PersonalAssistantApi.Application.DTOs.Tarefas;

public record TarefaDto
{
    public Guid Id { get; init; }
    public string Titulo { get; init; } = null!;

    public DateTime Data { get; init; }

    public bool Concluida { get; init; }
}
