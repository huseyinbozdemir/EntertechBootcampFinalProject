using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntertechFP.DAL.Concrete
{
    public class EntegratorEventDal : RepositoryBase<EntegratorEvent>, IEntegratorEventDal
    {
        public EntegratorEventDal(OnlineEventDbContext context) : base(context)
        {

        }
    }
}
