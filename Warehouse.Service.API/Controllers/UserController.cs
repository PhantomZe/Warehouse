using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Service.API.Data;
using Warehouse.Service.API.Models;
using Warehouse.Service.API.Models.Dto;

namespace Warehouse.Service.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;
        private ResponseDto responseDto;
        private IMapper mapper;

        public UserController(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
            this.responseDto = new ResponseDto();
        }

        [HttpPost]
        [Route("Login")]
        public ResponseDto Login([FromBody] UserDto userDto)
        {
            try
            {
                User user = db.users.First(x => x.Email.Equals(userDto.Email) && x.Password.Equals(userDto.Password) );
                responseDto.Result = mapper.Map<UserDto>(user);
                responseDto.IsSuccess = true;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }

        [HttpPost]
        [Route("Register")]
        public ResponseDto Register([FromBody] UserDto userDto)
        {
            try
            {
                User user = mapper.Map<User>(userDto);

                db.Add(user);
                db.SaveChanges();

                responseDto.Result = mapper.Map<UserDto>(user);
                responseDto.IsSuccess = true;
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.ErrorMessage = ex.Message;
            }
            return responseDto;
        }
        
    }
}
