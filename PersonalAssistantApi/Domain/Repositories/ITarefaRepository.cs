using PersonalAssistantApi.Domain.Entities;

namespace PersonalAssistantApi.Domain.Repositories;

public interface ITarefaRepository : IBaseRepository<Tarefa>
{
    Task<IEnumerable<Tarefa>> GetAllByUsuarioIdAsync(Guid usuarioId);
    Task<IEnumerable<Tarefa>> GetAllPendentesByUsuarioIdAsync(Guid usuarioId);
    Task<IEnumerable<Tarefa>> GetByRangeAsync(Guid usuarioId, DateTime inicio, DateTime fim);
}
