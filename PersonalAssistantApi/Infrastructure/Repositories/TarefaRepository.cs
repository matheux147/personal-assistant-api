using Microsoft.EntityFrameworkCore;
using PersonalAssistantApi.Domain.Entities;
using PersonalAssistantApi.Domain.Repositories;
using PersonalAssistantApi.Infrastructure.Persistence;

namespace PersonalAssistantApi.Infrastructure.Repositories;

public class TarefaRepository(AppDbContext context) : BaseRepository<Tarefa>(context), ITarefaRepository
{
    public async Task<IEnumerable<Tarefa>> GetAllByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Tarefas.AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId)
            .OrderBy(t => t.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> GetAllPendentesByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Tarefas.AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId && !t.Concluida)
            .OrderBy(t => t.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> GetByRangeAsync(Guid usuarioId, DateTime inicio, DateTime fim)
    {
        return await _context.Tarefas.AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId
                     && t.Data >= inicio
                     && t.Data <= fim)
            .OrderBy(t => t.Data)
            .ToListAsync();
    }
}