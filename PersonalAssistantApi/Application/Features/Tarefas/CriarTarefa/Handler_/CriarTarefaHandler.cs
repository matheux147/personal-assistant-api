using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Domain.Entities;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Tarefas.CriarTarefa;

public class CriarTarefaHandler(ITarefaRepository repository) : IRequestHandler<CriarTarefaCommand, Result<Guid>>
{
    private readonly ITarefaRepository _repository = repository;

    public async Task<Result<Guid>> Handle(CriarTarefaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tarefa = new Tarefa(request.Dto.UsuarioId, request.Dto.Titulo, request.Dto.Data);
            await _repository.AddAsync(tarefa);
            return Result<Guid>.Success(tarefa.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
    }
}