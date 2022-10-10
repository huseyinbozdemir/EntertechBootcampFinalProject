using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class EventAttendanceDal : RepositoryBase<EventAttendance>, IEventAttendanceDal
    {
        public EventAttendanceDal(DbContext context) : base(context)
        {

        }
    }
}
