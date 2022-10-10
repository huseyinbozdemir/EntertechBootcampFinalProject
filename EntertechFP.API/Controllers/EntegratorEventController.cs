using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Abstract;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EntegratorEventController : ControllerBase
    {
        private IEntegratorEventService entegratorEventService;
        public EntegratorEventController(IEntegratorEventDal entegratorEventDal, OnlineEventDbContext context)
        {
            this.entegratorEventService = new EntegratorEventService(entegratorEventDal,context);
        }

        [HttpGet]
        public BaseResponse<List<EntegratorEvent>> GetAll(int include = 0)
            => include == 0
            ? new BaseResponse<List<EntegratorEvent>>(entegratorEventService.GetAll())
            : new BaseResponse<List<EntegratorEvent>>(entegratorEventService.GetAll(null, c => c.Event, c => c.Entegrator));
    }
}
