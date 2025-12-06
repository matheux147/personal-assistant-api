using System.ComponentModel.DataAnnotations;

namespace PersonalAssistantApi.Application.DTOs.Tarefas;

public record CriarTarefaDto
{
    [Required]
    public Guid UsuarioId { get; init; }
    [Required] 
    public string Titulo { get; init; } = null!;
    [Required] 
    public DateTime Data { get; init; }
}
