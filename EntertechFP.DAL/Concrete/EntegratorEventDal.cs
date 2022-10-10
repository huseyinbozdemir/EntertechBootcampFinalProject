using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class EntegratorEventDal : RepositoryBase<EntegratorEvent>, IEntegratorEventDal
    {
        public EntegratorEventDal(DbContext context) : base(context)
        {

        }
    }
}
