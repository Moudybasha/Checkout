using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CheckoutCart.Data.Model.Core.Implementation
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private member variables...
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;
        #endregion

        #region Public Constructor...
        /// <summary>
        /// Public Constructor,initializes privately declared local variables.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbContext context)
        {
            Context = context;
            Context.Database.CommandTimeout = 180;
            DbSet = context.Set<TEntity>();


        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(bool tracking = false)
        {
            IQueryable<TEntity> query = tracking ? DbSet.AsNoTracking() : DbSet;
            return query.ToList();
        }

        public virtual DbSet<TEntity> GetDbSet()
        {
            return DbSet;

        }

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public virtual Task<TEntity> GetById(object id)
        {

            return DbSet.FindAsync(id);


        }
        public IEnumerable<T> SelectManyFromTable<T>(Expression<Func<TEntity, T>> predicate, bool tracking = true) where T : class
        {
            return tracking ? DbSet.Select(predicate) : DbSet.Select(predicate).AsNoTracking();
        }

        public IEnumerable<T> SelectDistinct<T>(Expression<Func<TEntity, T>> predicate, bool tracking = true) where T : class
        {
            return tracking ? DbSet.Select(predicate).Distinct() : DbSet.Select(predicate).AsNoTracking().Distinct();
        }


        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual TEntity Insert(TEntity entity)
        {

            return DbSet.Add(entity);

        }

        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity"></param>
        public virtual IEnumerable<TEntity> InsertMany(IEnumerable<TEntity> entity)
        {

            return DbSet.AddRange(entity);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Delete all the rows from table
        /// </summary>
        /// <param name="entityName"></param>
        public virtual void TruncateTable(string entityName)
        {
            Context.Database.ExecuteSqlCommand("TRUNCATE TABLE" + entityName);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteMany(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);

        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual void Update(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                DbSet.Attach(entityToUpdate);
            }
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, bool tracking = true)
        {

            return tracking ? DbSet.Where(where) : DbSet.Where(where).AsNoTracking();
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where, bool tracking = true)
        {
            return tracking ? DbSet.Where(where).AsQueryable() : DbSet.Where(where).AsQueryable().AsNoTracking();
        }

        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="tracking"></param>
        /// <returns></returns>
        public TEntity Get(Expression<Func<TEntity, bool>> where, bool tracking = true)
        {

            return tracking ? DbSet.Where(where).FirstOrDefault() : DbSet.Where(where).AsNoTracking().FirstOrDefault();
        }


        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            IQueryable<TEntity> objects = DbSet.Where(where).AsQueryable();
            foreach (TEntity obj in objects)
                DbSet.Remove(obj);
        }

        /// <summary>
        /// generic method to fetch all the records from db
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetAll(bool tracking = true)
        {
            return tracking ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();
        }

        /// <summary>
        /// Inclue multiple
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Exists(object primaryKey)
        {

            var existed = DbSet.FindAsync(primaryKey).Result;
            if (existed != null)
                Context.Entry(existed).State = EntityState.Detached;
            return existed != null;

        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate)
        {

            return DbSet.SingleAsync(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
        {

            return DbSet.FirstAsync(predicate);

        }

        public IQueryable<TEntity> GetTableAsQueryable(bool tracking = false)
        {

            return tracking ? DbSet.AsQueryable() : DbSet.AsQueryable().AsNoTracking();

        }

        public void Reload(TEntity entity)
        {
            Context.Entry(entity).Reload();

        }

        public void Attach(TEntity entity)
        {
            DbSet.Attach(entity);
        }

        public bool Any(Expression<Func<TEntity, bool>> any)
        {
            return DbSet.Any(any);
        }

        public void DeAttach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public void DeAttach(IEnumerable<TEntity> entities)
        {
           entities.ToList().ForEach(e=> Context.Entry(e).State = EntityState.Detached);
        }
        #endregion
    }
}
