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
    public class CityController : ControllerBase
    {
        private ICityService cityService;
        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }
        [HttpGet]
        public BaseResponse<List<City>> GetAll(int include = 0)
            => include == 0
            ? new BaseResponse<List<City>>(cityService.GetAll())
            : new BaseResponse<List<City>>(cityService.GetAll(null, c => c.Events));

        [HttpPost]
        public BaseResponse<City> Add([FromBody] City city)
        {
            if (cityService.Get(c => c.CityName.Equals(city.CityName)) is not null)
                return new BaseResponse<City>("Şehir mevcut.");
            cityService.Add(city);
            return new BaseResponse<City>(city);
        }
        [HttpPatch("{id}/{cityname}")]
        public BaseResponse<City> Update(int id, string cityName)
        {
            var city = cityService.Get(c => c.CityId == id);
            if (cityService.Get(c => c.CityName.Equals(cityName)) is not null)
                return new BaseResponse<City>("Şehir mevcut.");
            city.CityName = cityName;
            cityService.Update(city);
            return new BaseResponse<City>(city);
        }
        [HttpDelete("{id}")]
        public BaseResponse<City> DeleteCity(int id)
        {
            var city = cityService.Get(c => c.CityId == id);
            if (city is null)
                return new BaseResponse<City>("Bu id'ye ait şehir bulunamadı.");
            cityService.Delete(city);
            return new BaseResponse<City>(true);
        }
    }
}
