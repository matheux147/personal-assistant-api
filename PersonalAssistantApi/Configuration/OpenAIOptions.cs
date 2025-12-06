namespace PersonalAssistantApi.Configuration;

public class OpenAIOptions
{
    public const string SectionName = "OpenAI";
    public string ApiKey { get; set; } = null!;
    public string ModelId { get; set; } = null!;
}
