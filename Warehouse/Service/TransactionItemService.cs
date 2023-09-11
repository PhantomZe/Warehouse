using Warehouse.Models;
using Warehouse.Service.IService;
using Warehouse.Utility;

namespace Warehouse.Service
{
    public class TransactionItemService : ITransactionItemService
    {
        private readonly IBaseService baseService;
        public TransactionItemService(IBaseService baseService)
        {
            this.baseService = baseService;
        }
        public async Task<ResponseDto?> GetAllTransactionItemsAsync()
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.WarehouseAPIBase + "/api/TransactionItem"
            });
        }

        public async Task<ResponseDto?> InsertTransactionItemAsync(TransactionItemDto transactionItemDto)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = transactionItemDto,
                Url = SD.WarehouseAPIBase + "/api/TransactionItem/InsertTransactionItem"
            });
        }
    }
}
