using EntertechFP.BL.Abstract;
using EntertechFP.BL.Concrete;
using EntertechFP.DAL.Concrete;
using EntertechFP.DAL.Concrete.Contexts;
using EntertechFP.DAL.Concrete.Repositories;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IEventService eventService;
        private OnlineEventDbContext context;
        public TestController(OnlineEventDbContext context)
        {
            this.context = context;
            eventService = new EventService(new EventDal(context), context);
        }
        [HttpGet("GetAll")]
        public bool GetAll()
        {
            var repository = new RepositoryBase<Category>(context);
            var item = repository.GetAll(null, null, c => c.Events).ToList();
            return true;
        }
    }
}
