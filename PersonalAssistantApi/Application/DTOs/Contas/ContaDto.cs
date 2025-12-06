namespace PersonalAssistantApi.Application.DTOs.Contas;

public record ContaDto
{
    public Guid Id { get; init; }
    public string Descricao { get; init; } = null!;
    public decimal Valor { get; init; }
    public DateTime DataVencimento { get; init; }
    public bool Pago { get; init; }
}
