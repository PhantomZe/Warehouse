using Warehouse.Models;

namespace Warehouse.Service.IService
{
    public interface IUserService
    {
        Task<ResponseDto?> LoginAsync(UserDto userDto);
        Task<ResponseDto?> RegisterAsync(UserDto userDto);
    }
}
