namespace PersonalAssistantApi.Controllers;

public record BadRequestResponseDto
{
    public string Error { get; set; } = default!;
}
