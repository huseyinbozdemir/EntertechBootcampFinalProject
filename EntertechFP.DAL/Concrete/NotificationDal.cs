using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using System.Linq.Expressions;

namespace EntertechFP.DAL.Concrete
{
    internal class NotificationDal:RepositoryBase<Notification>, INotificationDal
    {
        public NotificationDal(OnlineEventDbContext context) : base(context)
        {

        }

        public void UpdateMany(Expression<Func<Notification,bool>> filter)
        {
            var userNotifications = context.Notifications.Where(filter).ToList();
            userNotifications.ForEach(n => n.IsSeen = true);
            context.UpdateRange(userNotifications);
            context.SaveChanges();
        }
    }
}
