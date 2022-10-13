using EntertechFP.API.Attributes;
using EntertechFP.API.Responses;
using EntertechFP.API.Utils;
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
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet("{mailaddress}/{password}")]
        public BaseResponse<User> Get(string mailAddress, string password)
        {
            var data = userService.Get(u => u.EmailAddress.Equals(mailAddress) && u.Password.Equals(Util.HashToMD5(password)));
            if (data is null)
                return new BaseResponse<User>("Kullanıcı bulunamadı");
            return new BaseResponse<User>(data);
        }
        [HttpGet("{id}")]
        public BaseResponse<User> Get(int id, int include = 0)
        {
            var data =
                (include == 0)
                ? userService.Get(u => u.UserId == id)
                : userService.Get(u => u.UserId == id, u => u.Events, u => u.Notifications);
            if (data is null)
                return new BaseResponse<User>("Kullanıcı bulunamadı");
            return new BaseResponse<User>(data);
        }
        [HttpGet]
        public BaseResponse<List<User>> GetAll(int include = 0)
             => (include == 0)
                ? new BaseResponse<List<User>>(userService.GetAll())
                : new BaseResponse<List<User>>(userService.GetAll(null, u => u.Events));
        [HttpPost]
        public BaseResponse<User> Add([FromBody] User user)
        {
            var data = userService.Get(u => u.EmailAddress.Equals(user.EmailAddress));
            if (data is null)
            {
                user.Password = Util.HashToMD5(user.Password);
                userService.Add(user);
                return new BaseResponse<User>(user);
            }
            return new BaseResponse<User>("Bu mail zaten mevcut.");
        }
        [HttpPut("{id}")]
        public BaseResponse<User> Update(int id, [FromBody] User user)//
        {
            var current = Get(id).Data;
            if (current is null)
                return new BaseResponse<User>("Kullanıcı bulunamadı.");
            if (!current.EmailAddress.Equals(user.EmailAddress) && userService.Get(u => u.EmailAddress.Equals(user.EmailAddress)) is not null)
                return new BaseResponse<User>("Böyle bir mail adresi mevcut");
            current.FirstName = user.FirstName;
            current.LastName = user.LastName;
            current.Password = (current.Password.Equals(Util.HashToMD5(user.Password))) ? current.Password : Util.HashToMD5(user.Password);
            current.EmailAddress = user.EmailAddress;
            userService.Update(current);
            return new BaseResponse<User>(current);
        }
        [HttpDelete("{id}")]
        public BaseResponse<User> Delete(int id)
        {
            var current = Get(id).Data;
            if (current is null)
                return new BaseResponse<User>("Kullanıcı bulunamadı");
            userService.Delete(current);
            return new BaseResponse<User>(true);

        }
    }
}
