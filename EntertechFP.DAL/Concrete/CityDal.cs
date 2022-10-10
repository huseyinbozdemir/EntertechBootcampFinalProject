using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    public class CityDal : RepositoryBase<City>, ICityDal
    {
        public CityDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
