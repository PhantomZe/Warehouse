using Warehouse.Models;

namespace Warehouse.Service.IService
{
    public interface IItemService
    {
        Task<ResponseDto?> GetItemByNameAsync(string itemName);
        Task<ResponseDto?> GetAllItemsAsync();
        Task<ResponseDto?> GetItemByIdAsync(int id);
        Task<ResponseDto?> InsertUpdateItemsAsync(ItemDto itemDto);
        Task<ResponseDto?> DeleteItemAsync(int id);
    }
}
