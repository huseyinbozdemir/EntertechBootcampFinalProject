using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    internal class EntegratorEventDal : RepositoryBase<EntegratorEvent>, IEntegratorEventDal
    {
        public EntegratorEventDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
