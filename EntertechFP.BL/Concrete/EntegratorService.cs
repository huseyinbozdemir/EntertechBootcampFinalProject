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
    public class EntegratorService : BaseService<Entegrator>,IEntegratorService
    {
        private IEntegratorDal entegratorDal;
        private OnlineEventDbContext context;

        public EntegratorService(IEntegratorDal entegratorDal, OnlineEventDbContext context) : base(context)
        {
            this.entegratorDal = entegratorDal;
            this.context = context;
        }
    }
}
