using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;

namespace EntertechFP.DAL.Concrete
{
    public class UserDal : RepositoryBase<User>, IUserDal
    {
        public UserDal(DbContext context) : base(context)
        {

        }
    }
}
