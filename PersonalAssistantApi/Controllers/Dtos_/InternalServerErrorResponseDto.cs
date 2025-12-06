namespace PersonalAssistantApi.Controllers;

public record InternalServerErrorResponseDto
{
    public string Message { get; set; } = "Erro interno no servidor.";
}
