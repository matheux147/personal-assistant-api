namespace PersonalAssistantApi.Controllers;

public record AiResponseDto
{
    public string Resposta { get; set; } = default!;
    public DateTime Data { get; set; }
}

