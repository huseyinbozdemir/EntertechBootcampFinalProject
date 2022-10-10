﻿using EntertechFP.BL.Abstract;
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
    public class EntegratorEventService : BaseService<EntegratorEvent>, IEntegratorEventService
    {
        private IEntegratorEventDal entegratorEventDal;
        private OnlineEventDbContext context;

        public EntegratorEventService(IEntegratorEventDal entegratorEventDal, OnlineEventDbContext context) : base(context)
        {
            this.entegratorEventDal = entegratorEventDal;
            this.context = context;
        }
    }
}
