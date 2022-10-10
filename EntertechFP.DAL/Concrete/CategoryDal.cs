using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class CategoryDal:RepositoryBase<Category>,ICategoryDal
    {
        public CategoryDal(DbContext context) : base(context)
        {

        }
    }
}
