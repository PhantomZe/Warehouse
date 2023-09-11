using Warehouse.Models;
using Warehouse.Service.IService;
using Warehouse.Utility;

namespace Warehouse.Service
{
    public class UserService : IUserService
    {
        private readonly IBaseService baseService;
        public UserService(IBaseService baseService)
        {
            this.baseService = baseService;
        }
        public async Task<ResponseDto?> LoginAsync(UserDto userDto)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = userDto,
                Url = SD.WarehouseAPIBase + "/api/user/Login"
            });
        }

        public async Task<ResponseDto?> RegisterAsync(UserDto userDto)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = userDto,
                Url = SD.WarehouseAPIBase + "/api/user/Register"
            });
        }
    }
}
