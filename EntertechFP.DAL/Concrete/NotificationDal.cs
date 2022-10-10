using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class NotificationDal:RepositoryBase<Notification>, INotificationDal
    {
        public NotificationDal(DbContext context) : base(context)
        {

        }
    }
}
