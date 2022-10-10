using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;

namespace EntertechFP.DAL.Concrete
{
    public class UserDal : RepositoryBase<User>, IUserDal
    {
        public UserDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
