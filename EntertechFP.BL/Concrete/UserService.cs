using EntertechFP.BL.Abstract;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntertechFP.BL.Concrete
{
    public class UserService : BaseService<User>, IUserService
    {
        private IUserDal userDal;
        private OnlineEventDbContext context;

        public UserService(IUserDal userDal, OnlineEventDbContext context) : base(context)
        {
            this.userDal = userDal;
            this.context = context;
        }

    }
}
