using Warehouse.Models;

namespace Warehouse.Service.IService
{
    public interface ITransactionItemService
    {
        Task<ResponseDto?> GetAllTransactionItemsAsync();
        Task<ResponseDto?> InsertTransactionItemAsync(TransactionItemDto transactionItemDto);
    }
}
