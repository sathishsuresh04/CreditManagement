using CreditManagement.Application.Abstractions.Data;
using CreditManagement.Domain.Primitives;
using CreditManagement.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CreditManagement.Persistence.Repositories;

public abstract class GenericRepository<TEntity>
where TEntity : Entity
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="GenericRepository{TEntity}" /> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    protected GenericRepository(IDbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <summary>
    ///     Gets the database context.
    /// </summary>
    protected IDbContext DbContext { get; }

    /// <summary>
    ///     Gets the entity with the specified identifier.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    /// <returns>The maybe instance that may contain the entity with the specified identifier.</returns>
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await DbContext.GetBydIdAsync<TEntity>(id);
    }

    /// <summary>
    ///     Inserts the specified entity into the database.
    /// </summary>
    /// <param name="entity">The entity to be inserted into the database.</param>
    public void Insert(TEntity entity)
    {
        DbContext.Insert(entity);
    }

    /// <summary>
    ///     Inserts the specified entities into the database.
    /// </summary>
    /// <param name="entities">The entities to be inserted into the database.</param>
    public void InsertRange(IReadOnlyCollection<TEntity> entities)
    {
        DbContext.InsertRange(entities);
    }

    /// <summary>
    ///     Updates the specified entity in the database.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    /// <summary>
    ///     Removes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to be removed from the database.</param>
    public void Remove(TEntity entity)
    {
        DbContext.Remove(entity);
    }

    /// <summary>
    ///     Checks if any entity meets the specified specification.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns>True if any entity meets the specified specification, otherwise false.</returns>
    public async Task<bool> AnyAsync(Specification<TEntity> specification)
    {
        return await DbContext.Set<TEntity>().AnyAsync(specification);
    }

    /// <summary>
    ///     Asynchronously inserts the specified entities into the database.
    /// </summary>
    /// <param name="entities">The entities to be inserted into the database.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities);
    }

    /// <summary>
    ///     Gets the first entity that meets the specified specification.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns>The maybe instance that may contain the first entity that meets the specified specification.</returns>
    public async Task<TEntity?> FirstOrDefaultAsync(Specification<TEntity> specification)
    {
        return await DbContext.Set<TEntity>().FirstOrDefaultAsync(specification);
    }
}
