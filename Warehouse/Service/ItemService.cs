using Warehouse.Models;
using Warehouse.Service.IService;
using Warehouse.Utility;

namespace Warehouse.Service
{
    public class ItemService : IItemService
    {
        private readonly IBaseService baseService;
        public ItemService(IBaseService baseService) 
        {
            this.baseService = baseService;
        }


        public async Task<ResponseDto?> GetItemByNameAsync(string itemName)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.WarehouseAPIBase + "/api/Item/GetByName/" + itemName
            });
        }

        public async Task<ResponseDto?> GetAllItemsAsync()
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.WarehouseAPIBase + "/api/Item"
            });
        }

        public async Task<ResponseDto?> GetItemByIdAsync(int id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.WarehouseAPIBase + "/api/Item/" + id
            });
        }

        public async Task<ResponseDto?> InsertUpdateItemsAsync(ItemDto itemDto)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = itemDto,
                Url = SD.WarehouseAPIBase + "/api/Item/InsertUpdateItem"
            });
        }

        public async Task<ResponseDto?> DeleteItemAsync(int id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.WarehouseAPIBase + "/api/Item/DeleteItem/" + id
            });
        }
    }
}
