using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    internal class EventDal : RepositoryBase<Event>, IEventDal
    {
        public EventDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
