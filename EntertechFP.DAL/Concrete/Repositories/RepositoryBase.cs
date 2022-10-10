using EntertechFP.DAL.Abstract.Repositories;
using EntertechFP.DAL.Concrete.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntertechFP.DAL.Concrete.Repositories
{
    public class RepositoryBase<Entity> : IEntityRepository<Entity>
        where Entity : class, new()
    {
        protected OnlineEventDbContext context;
        public RepositoryBase(OnlineEventDbContext context)
        {
            this.context = context;
        }
        public Entity Get(Expression<Func<Entity, bool>> filter)
            => context.Set<Entity>().FirstOrDefault(filter);

        public List<Entity> GetAll(Expression<Func<Entity, bool>> filter = null) 
            => filter == null ? context.Set<Entity>().ToList() : context.Set<Entity>().Where(filter).ToList();
        public Entity Get(Expression<Func<Entity, bool>> filter, params Expression<Func<Entity, object>>[] includeProperties)
        {
            IQueryable<Entity> query = context.Set<Entity>().Where(filter);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();
        }
        public List<Entity> GetAll(Expression<Func<Entity, bool>> filter = null, params Expression<Func<Entity, object>>[] includeProperties)
        {
            IQueryable<Entity> query = context.Set<Entity>();
            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }
        public void Add(Entity entity)
        {
            context.Add(entity);
            context.SaveChanges();
        }
        public void Delete(Entity entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }
        public void Update(Entity entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }
    }
}
