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

        [HttpGet("{id}")]
        public BaseResponse<City> Get(int id, int include = 0)
            => include == 0
            ? new BaseResponse<City>(cityService.Get(c => c.CityId == id))
            : new BaseResponse<City>(cityService.Get(c => c.CityId == id, c => c.Events));

        [HttpPost]
        public BaseResponse<City> Add([FromBody] City city)
        {
            if (cityService.Get(c => c.CityName.Equals(city.CityName)) is not null)
                return new BaseResponse<City>("Şehir mevcut.");
            cityService.Add(city);
            return new BaseResponse<City>(city);
        }
        [HttpPatch("{id}")]
        public BaseResponse<City> Update(int id,[FromBody] City city)
        {
            var updatedCity = Get(id).Data;
            if (cityService.Get(c => c.CityName.Equals(city.CityName)) is not null)
                return new BaseResponse<City>("Şehir mevcut.");
            updatedCity.CityName = city.CityName;
            cityService.Update(updatedCity);
            return new BaseResponse<City>(updatedCity);
        }
        [HttpDelete("{id}")]
        public BaseResponse<City> DeleteCity(int id)
        {
            var city = Get(id).Data;
            if (city is null)
                return new BaseResponse<City>("Bu id'ye ait şehir bulunamadı.");
            cityService.Delete(city);
            return new BaseResponse<City>(true);
        }
    }
}
