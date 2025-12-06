using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Application.Common.Interfaces;
using PersonalAssistantApi.Configuration;
using PersonalAssistantApi.Domain.Repositories;
using PersonalAssistantApi.Infrastructure.Middleware;
using PersonalAssistantApi.Infrastructure.Persistence;
using PersonalAssistantApi.Infrastructure.Repositories;
using PersonalAssistantApi.Services.SemanticKernel;
using PersonalAssistantApi.Services.SemanticKernel.Functions;
using PersonalAssistantApi.Services.SemanticKernel.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection(OpenAIOptions.SectionName));
var openAiOptions = builder.Configuration.GetSection(OpenAIOptions.SectionName).Get<OpenAIOptions>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<ContasFunctions>();
builder.Services.AddScoped<TarefasFunctions>();

builder.Services.AddTransient<Kernel>(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();

    kernelBuilder.Services.AddOpenAIChatCompletion(
        modelId: openAiOptions!.ModelId,
        apiKey: openAiOptions.ApiKey
    );

    var contasFunctions = sp.GetRequiredService<ContasFunctions>();
    var tarefasFunctions = sp.GetRequiredService<TarefasFunctions>();

    kernelBuilder.Plugins.AddFromObject(contasFunctions, "Contas");
    kernelBuilder.Plugins.AddFromObject(tarefasFunctions, "Tarefas");

    return kernelBuilder.Build();
});

builder.Services.AddScoped<IApiResponseHandler, ApiResponseHandler>();
builder.Services.AddScoped<IAiService, AiService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
        p.AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
});

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

var app = builder.Build();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
