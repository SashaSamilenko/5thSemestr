using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Interfaces
{
    /// <summary>
    /// Interface of entity repository
    /// </summary>
    /// <typeparam name="TEntity">Type of Entity</typeparam>
    public interface IRepository<TEntity> where TEntity: class
    {
        /// <summary>
        /// Get all entities from context
        /// </summary>
        /// <returns>Entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get entity from context with given identifier
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Entity</returns>
        TEntity Get(int id);

        /// <summary>
        /// Add given entity into DB context
        /// </summary>
        /// <param name="item"></param>
        void Create(TEntity item);

        /// <summary>
        /// Change EntityState to EntityState.Modified=16
        /// </summary>
        /// <param name="item"></param>
        void Update(TEntity item);

        /// <summary>
        /// Change EntityState to EntityState.Deleted=8 and after next calling of method SaveChanges() will be deleted from DB.
        /// If entity does not exist into DB return null
        /// If the entity exists in the database context but has EntityState.Added = 4,
        /// this method will detach given entity from context
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Finds entities that satisfy the condition
        /// </summary>
        /// <param name="predicate">Condition of finding</param>
        /// <returns>Enumerable of entities</returns>
        IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate);
    }
}
