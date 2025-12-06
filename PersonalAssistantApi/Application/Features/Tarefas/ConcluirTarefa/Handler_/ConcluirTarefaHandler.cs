using MediatR;
using PersonalAssistantApi.Application.Common;
using PersonalAssistantApi.Domain.Repositories;

namespace PersonalAssistantApi.Application.Features.Tarefas.ConcluirTarefa;

public class ConcluirTarefaHandler(ITarefaRepository repository)
    : IRequestHandler<ConcluirTarefaCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(ConcluirTarefaCommand request, CancellationToken cancellationToken)
    {
        var tarefa = await repository.GetByIdAsync(request.TarefaId);

        if (tarefa == null)
            return Result<bool>.Failure("Tarefa não encontrada.");

        if (tarefa.UsuarioId != request.UsuarioId)
            return Result<bool>.Failure("Tarefa não pertence ao usuário.");

        tarefa.Concluir();

        await repository.UpdateAsync(tarefa);

        return Result<bool>.Success(true);
    }
}