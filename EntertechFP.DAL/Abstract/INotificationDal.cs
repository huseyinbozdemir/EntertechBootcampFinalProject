using EntertechFP.DAL.Abstract.Repositories;
using EntertechFP.EL.Concrete;
using System.Linq.Expressions;

namespace EntertechFP.DAL.Abstract
{
    public interface INotificationDal : IEntityRepository<Notification>
    {
        void UpdateMany(Expression<Func<Notification, bool>> filter);
    }
}
