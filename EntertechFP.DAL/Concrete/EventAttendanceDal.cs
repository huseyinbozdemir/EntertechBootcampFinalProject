using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    public class EventAttendanceDal : RepositoryBase<EventAttendance>, IEventAttendanceDal
    {
        public EventAttendanceDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
