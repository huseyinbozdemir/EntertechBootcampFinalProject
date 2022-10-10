using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class EntegratorDal : RepositoryBase<Entegrator>, IEntegratorDal
    {
        public EntegratorDal(DbContext context) : base(context)
        {

        }
    }
}
