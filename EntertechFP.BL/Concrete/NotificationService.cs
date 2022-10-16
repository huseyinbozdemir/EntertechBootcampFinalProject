using EntertechFP.BL.Abstract;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using System.Linq.Expressions;

namespace EntertechFP.BL.Concrete
{
    internal class NotificationService : BaseService<Notification>, INotificationService
    {
        private INotificationDal notificationDal;
        public NotificationService(INotificationDal notificationDal, OnlineEventDbContext context) : base(context)
        {
            this.notificationDal = notificationDal;
        }
        public void UpdateMany(Expression<Func<Notification, bool>> filter)
        {
            notificationDal.UpdateMany(filter);
        }
    }
}
