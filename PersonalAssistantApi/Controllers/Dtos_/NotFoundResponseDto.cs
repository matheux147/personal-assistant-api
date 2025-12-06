namespace PersonalAssistantApi.Controllers;

public record NotFoundResponseDto
{
    public string Message { get; set; } = default!;
}