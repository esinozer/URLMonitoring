using Core.DataAccsess;
using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
         where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var contex = new TContext())
            {
                var addedEntity = contex.Entry(entity);
                addedEntity.State = EntityState.Added;
                contex.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var contex = new TContext())
            {
                var deleteEntity = contex.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                contex.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var contex = new TContext())
            {
                return contex.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public IList<TEntity> Getlist(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var contex = new TContext())
            {
                return filter == null
                    ? contex.Set<TEntity>().ToList()
                    : contex.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var contex = new TContext())
            {
                var updateEntity = contex.Entry(entity);
                updateEntity.State = EntityState.Modified;
                contex.SaveChanges();
            }
        }


    }
}
