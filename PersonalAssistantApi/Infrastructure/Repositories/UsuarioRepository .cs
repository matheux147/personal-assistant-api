using Microsoft.EntityFrameworkCore;
using PersonalAssistantApi.Domain.Entities;
using PersonalAssistantApi.Domain.Repositories;
using PersonalAssistantApi.Infrastructure.Persistence;

namespace PersonalAssistantApi.Infrastructure.Repositories;

public class UsuarioRepository(AppDbContext context) : BaseRepository<Usuario>(context), IUsuarioRepository
{
    public async Task<Usuario?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Usuarios
            .Include(u => u.Tarefas)
            .Include(u => u.Contas)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}