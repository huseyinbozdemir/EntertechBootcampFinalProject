using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.BL.Abstract;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EntegratorEventController : ControllerBase
    {
        private IEntegratorEventService entegratorEventService;
        public EntegratorEventController(IEntegratorEventService entegratorEventService)
        {
            this.entegratorEventService = entegratorEventService;
        }

        [HttpGet("{id}")]
        public BaseResponse<List<EntegratorEvent>> GetAll(int id, int include = 0)
            => include == 0
            ? new BaseResponse<List<EntegratorEvent>>(entegratorEventService.GetAll(e => e.EventId == id))
            : new BaseResponse<List<EntegratorEvent>>(entegratorEventService.GetAll(e => e.EventId == id, c => c.Entegrator));
    }
}
