using EntertechFP.BL.Abstract;
using EntertechFP.DAL.Abstract.Repositories;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using System.Linq.Expressions;

namespace EntertechFP.BL.Concrete
{
    internal abstract class BaseService<T> : IBaseService<T>
        where T : class, new()
    {
        protected readonly IEntityRepository<T> repository;
        public BaseService(OnlineEventDbContext context)
        {
            repository = new RepositoryBase<T>(context);
        }
        public virtual void Add(T entity)
            => repository.Add(entity);
        public virtual void Delete(T entity)
            => repository.Delete(entity);
        public virtual void Update(T entity)
            => repository.Update(entity);
        public virtual T Get(Expression<Func<T, bool>> filter)
            => repository.Get(filter);
        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
            => repository.GetAll(filter);

        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties)
            => repository.GetAll(filter, includeProperties);

        public virtual T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
            => repository.Get(filter, includeProperties);
    }
}
