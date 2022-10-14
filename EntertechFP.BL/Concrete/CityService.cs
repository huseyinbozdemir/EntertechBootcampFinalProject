using EntertechFP.BL.Abstract;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntertechFP.BL.Concrete
{
    internal class CityService : BaseService<City>, ICityService
    {
        private ICityDal cityDal;

        public CityService(ICityDal cityDal, OnlineEventDbContext context) : base(context)
        {
            this.cityDal = cityDal;
        }
    }
}
