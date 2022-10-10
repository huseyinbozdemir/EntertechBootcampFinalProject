﻿using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    public class NotificationDal:RepositoryBase<Notification>, INotificationDal
    {
        public NotificationDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
