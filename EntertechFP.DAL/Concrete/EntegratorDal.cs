using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    public class EntegratorDal : RepositoryBase<Entegrator>, IEntegratorDal
    {
        public EntegratorDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
