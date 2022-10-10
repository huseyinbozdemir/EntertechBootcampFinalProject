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
    public class EventAttendanceService : BaseService<EventAttendance>,IEventAttendanceService
    {
        private IEventAttendanceDal eventAttendanceDal;
        private OnlineEventDbContext context;

        public EventAttendanceService(IEventAttendanceDal eventAttendanceDal, OnlineEventDbContext context) : base(context)
        {
            this.eventAttendanceDal = eventAttendanceDal;
            this.context = context;
        }
    }
}
