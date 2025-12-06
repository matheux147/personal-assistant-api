using System.ComponentModel.DataAnnotations;

namespace PersonalAssistantApi.Application.DTOs.Contas;

public class CriarContaDto
{
    [Required]
    public Guid UsuarioId { get; set; }

    [Required]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    public decimal Valor { get; set; }

    [Required]
    public DateTime DataVencimento { get; set; }
}
