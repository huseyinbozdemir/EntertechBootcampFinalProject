using EntertechFP.BL.Abstract;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntertechFP.BL.Concrete
{
    public class NotificationService : BaseService<Notification>, INotificationService
    {
        private INotificationDal notificationDal;
        private OnlineEventDbContext context;

        public NotificationService(INotificationDal notificationDal, OnlineEventDbContext context) : base(context)
        {
            this.notificationDal = notificationDal;
            this.context = context;
        }
    }
}
