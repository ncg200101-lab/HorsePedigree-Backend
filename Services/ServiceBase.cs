using HorsePedigree_2026.Exceptions;
using HorsePedigree_2026.Repositories;

namespace HorsePedigree_2026.Services;

public abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : class
{
    protected readonly IRepository<TEntity> Repository;

    protected ServiceBase(IRepository<TEntity> repository)
    {
        Repository = repository;
    }

    public virtual Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return Repository.GetByIdAsync(id, cancellationToken);
    }

    public virtual Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Repository.GetAllAsync(cancellationToken);
    }

    public virtual Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Repository.AddAsync(entity, cancellationToken);
    }

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return Repository.UpdateAsync(entity, cancellationToken);
    }

    public virtual async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(typeof(TEntity).Name, id);
        }

        await Repository.DeleteAsync(entity, cancellationToken);
    }
}
