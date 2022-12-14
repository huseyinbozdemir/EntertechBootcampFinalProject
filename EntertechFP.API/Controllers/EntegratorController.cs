using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.API.Utils;
using EntertechFP.BL.Abstract;
using EntertechFP.EL.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace EntertechFP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class EntegratorController : ControllerBase
    {
        private IEntegratorService entegratorService;
        public EntegratorController(IEntegratorService entegratorService)
        {
            this.entegratorService = entegratorService;
        }
        [HttpGet("{mailaddress}/{password}")]
        public BaseResponse<Entegrator> Get(string mailAddress, string password)
        {
            var data = entegratorService.Get(e => e.EmailAdress.Equals(mailAddress) && e.Password.Equals(Util.HashToMD5(password)));
            if (data is null)
                return new BaseResponse<Entegrator>("Entegratör bulunamadı.");
            return new BaseResponse<Entegrator>(data);
        }
        [HttpGet("{id}")]
        public BaseResponse<Entegrator> Get(int id)
        {
            var data = entegratorService.Get(e=>e.EntegratorId==id);
            if (data is null)
                return new BaseResponse<Entegrator>("Entegratör bulunamadı.");
            return new BaseResponse<Entegrator>(data);
        }
        [HttpGet]
        public BaseResponse<List<Entegrator>> GetAll()
            => new BaseResponse<List<Entegrator>>(entegratorService.GetAll());

        [HttpPost]
        public BaseResponse<Entegrator> Add([FromBody] Entegrator entegrator)
        {
            var data = entegratorService.Get(e => e.EmailAdress.Equals(entegrator.EmailAdress));
            if (data is not null)
                return new BaseResponse<Entegrator>("Bu mail'e kayıtlı kullanıcı mevcut.");
            entegrator.ApiKey = Util.CreateApiKey();
            entegrator.Password = Util.HashToMD5(entegrator.Password);
            entegratorService.Add(entegrator);
            return new BaseResponse<Entegrator>(entegrator);
        }
        [HttpDelete("{id}")]
        public BaseResponse<Entegrator> Delete(int id)
        {
            var entegrator = entegratorService.Get(e => e.EntegratorId == id);
            if (entegrator is null)
                return new BaseResponse<Entegrator>("Entegratör bulunamadı.");
            entegratorService.Delete(entegrator);
            return new BaseResponse<Entegrator>(true);
        }

    }
}
