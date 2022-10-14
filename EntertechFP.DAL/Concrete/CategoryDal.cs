using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    internal class CategoryDal:RepositoryBase<Category>,ICategoryDal
    {
        public CategoryDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
