using EntertechFP.EL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntertechFP.BL.Abstract
{
    public interface INotificationService : IBaseService<Notification>
    {
        void UpdateMany(Expression<Func<Notification, bool>> filter);
    }
}
