using Microsoft.EntityFrameworkCore;
using PersonalAssistantApi.Domain.Entities;
using PersonalAssistantApi.Domain.Repositories;
using PersonalAssistantApi.Infrastructure.Persistence;

namespace PersonalAssistantApi.Infrastructure.Repositories;

public class ContaRepository(AppDbContext context) : BaseRepository<Conta>(context), IContaRepository
{
    public async Task<IEnumerable<Conta>> GetAllByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Contas.AsNoTracking()
            .Where(t => t.UsuarioId == usuarioId)
            .OrderBy(t => t.DataVencimento)
            .ToListAsync();
    }

    public async Task<IEnumerable<Conta>> GetByVencimentoAsync(Guid usuarioId, DateTime inicio, DateTime fim)
    {
        return await _context.Contas.AsNoTracking()
            .Where(c => c.UsuarioId == usuarioId && c.DataVencimento.Date >= inicio.Date && c.DataVencimento.Date <= fim.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Conta>> GetPendentesByUsuarioIdAsync(Guid usuarioId)
    {
        return await _context.Contas.AsNoTracking()
            .Where(c => c.UsuarioId == usuarioId && !c.Pago)
            .OrderBy(c => c.DataVencimento)
            .ToListAsync();
    }
}