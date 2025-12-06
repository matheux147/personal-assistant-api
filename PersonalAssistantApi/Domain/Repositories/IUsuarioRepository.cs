using PersonalAssistantApi.Domain.Entities;

namespace PersonalAssistantApi.Domain.Repositories;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> GetByIdWithDetailsAsync(Guid id);
}
