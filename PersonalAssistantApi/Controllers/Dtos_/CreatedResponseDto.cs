namespace PersonalAssistantApi.Controllers;

public record CreatedResponseDto
{
    public Guid Id { get; set; }
    public string Message { get; set; } = default!;
}
