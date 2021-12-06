using PlannerTasks.DAL.EF;
using PlannerTasks.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerTasks.DAL.Repositories
{
    /// <summary>
    /// Class EFRepository.
    /// Implements interface IRepository with type value of entity with TEntity type.
    /// Provides method to interact with DB context.
    /// </summary>
    public class EFRepository<TEntity>: IRepository<TEntity> where TEntity: class
    {
        /// <summary>
        /// DB context.
        /// </summary>
        private PlannerContext db;

        /// <summary>
        /// DB set
        /// </summary>
        private DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor with one parameter
        /// </summary>
        /// <param name="context">Context of DB</param>
        public EFRepository(PlannerContext context)
        {
            this.db = context;
            _dbSet = db.Set<TEntity>();
        }

        /// <summary>
        /// Method return IEnumerable of TEnity entities
        /// </summary>
        /// <returns>IEnumerable of TEntity entities</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get entities of type TEntity with given Id
        /// </summary>
        /// <param name="id">Identifier of entity</param>
        /// <returns>Entities with type TEntity with given Id</returns>
        public TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Adds entity to DB context.
        /// Changes EntityState from Detached=1 to Added=4 for TEnity entity
        /// </summary>
        /// <param name="task">Task entity for adding</param>
        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Changes EntityState to Modified=16 for TEnity entity
        /// </summary>
        /// <param name="entity">Modified TEntity entity</param>
        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Finds TEntity entities that satisfy the condition
        /// </summary>
        /// <param name="predicate">Condition of finding</param>
        /// <returns>Enumerable of TEntity entities
        /// which obey condition(predicate)</returns>
        public IEnumerable<TEntity> Find(Func<TEntity, Boolean> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        /// <summary>
        /// Remove TEntity entity from DBContext and DB with given Id if that entity exists
        /// </summary>
        /// <param name="id">Identifier of TEntity entity</param>
        public void Delete(int id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }
    }
}