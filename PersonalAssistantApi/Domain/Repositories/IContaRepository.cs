using PersonalAssistantApi.Domain.Entities;

namespace PersonalAssistantApi.Domain.Repositories;

public interface IContaRepository : IBaseRepository<Conta>
{
    Task<IEnumerable<Conta>> GetAllByUsuarioIdAsync(Guid usuarioId);
    Task<IEnumerable<Conta>> GetByVencimentoAsync(Guid usuarioId, DateTime inicio, DateTime fim, bool? somentePendentes = null);
    Task<IEnumerable<Conta>> GetPendentesByUsuarioIdAsync(Guid usuarioId);
}
