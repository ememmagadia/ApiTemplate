using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastracture.Persistence
{
    public class RepositoryBase<TEntity>
        : IRepository<TEntity> where TEntity : class
    {
        private readonly IApiDbContext context;

        public RepositoryBase(IApiDbContext context)
        {
            this.context = context;
        }

        public TEntity Create(TEntity entitity)
        {
            this.context.Set<TEntity>().Add(entitity);
            this.context.SaveChanges();
            return entitity;
        }

        public void Delete(Guid id)
        {
            var found = this.Retrieve(id);
            this.context.Set<TEntity>().Remove(found);
            this.context.SaveChanges();
        }

        public TEntity Retrieve(Guid id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> Retrieve()
        {
            return this.context.Set<TEntity>().ToList();
        }

        public TEntity Update(Guid id, TEntity entity)
        {
            this.context.Update(entity);
            this.context.SaveChanges();
            return entity;
        }
    }
}
