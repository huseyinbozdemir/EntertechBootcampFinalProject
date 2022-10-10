using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class CityDal : RepositoryBase<City>, ICityDal
    {
        public CityDal(DbContext context) : base(context)
        {

        }
    }
}
