using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context.Extensions
{
    public abstract class RepositoryBase<TDomainModel, TId, TEntity, TDbContext>(TDbContext context)
        : IRepositoryBase<TDomainModel, TId>
        where TEntity : class
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context = context;
        protected DbSet<TEntity> Set => _context.Set<TEntity>();

        protected abstract TId GetId(TDomainModel model);
        protected abstract TEntity ToEntity(TDomainModel model);
        protected abstract TDomainModel ToDomainModel(TEntity entity);
        protected abstract void ApplyPropertyUpdated(TEntity entity, TDomainModel model);

        public virtual async Task AddAsync(TDomainModel model, CancellationToken ct = default)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var entity = ToEntity(model);
            await Set.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
        }

        public virtual async Task<bool> UpdateAsync(TDomainModel model, CancellationToken ct = default)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var id = GetId(model);
            var entity = await Set.FindAsync([id], ct);

            // FIX: Vi ska returnera false om den INTE finns
            if (entity is null) return false;

            ApplyPropertyUpdated(entity, model);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public virtual async Task<bool> RemoveAsync(TDomainModel model, CancellationToken ct = default)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            var id = GetId(model);
            var entity = await Set.FindAsync([id], ct);

            if (entity is null) return false;

            Set.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return true;
        }

        public virtual async Task<TDomainModel?> GetByIdAsync(TId id, CancellationToken ct = default)
        {
            var entity = await Set.FindAsync([id], ct);
            return entity is null ? default : ToDomainModel(entity);
        }

        public virtual async Task<IReadOnlyList<TDomainModel>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await Set.AsNoTracking().ToListAsync(ct);
            return entities.Select(ToDomainModel).ToList();
        }
    }
}