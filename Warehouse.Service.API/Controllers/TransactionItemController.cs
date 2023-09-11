using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Service.API.Data;
using Warehouse.Service.API.Models;
using Warehouse.Service.API.Models.Dto;

namespace Warehouse.Service.API.Controllers
{
    [Route("api/TransactionItem")]
    [ApiController]
    public class TransactionItemController : ControllerBase
    {
        private readonly AppDbContext db;
        private ResponseDto responseDto;
        private IMapper mapper;

        public TransactionItemController(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
            this.responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto GetAllTransactionItem()
        {
            try
            {
                List<TransactionListItemDto> listTransaction = (from a in db.transaction_items
                                                     join u in db.users on a.UserID equals u.ID
                                                     join i in db.items on a.ItemID equals i.ItemID
                                                     select new TransactionListItemDto
                                                     {
                                                         TransactionItemID = a.TransactionItemID,
                                                         ItemID = a.ItemID,
                                                         ItemAmount = a.ItemAmount,
                                                         ItemName = i.ItemName,
                                                         ItemPrice = a.ItemPrice,
                                                         RecipientName = a.RecipientName,
                                                         TransactionDate = a.TransactionDate,
                                                         UserID = a.UserID,
                                                         UserName = u.Name
                                                     }).ToList();
                responseDto.Result = listTransaction;
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
        [Route("InsertTransactionItem")]
        public ResponseDto InsertTransactionItem([FromBody] TransactionItemDto transactionItemDto)
        {
            try
            {
                Item item = db.items.Where(x => x.ItemID==transactionItemDto.ItemID).FirstOrDefault();
                if (item != null && item.ItemID != 0 && item.ItemAmount>=transactionItemDto.ItemAmount)
                {
                    item.ItemAmount = item.ItemAmount-transactionItemDto.ItemAmount;
                    db.Update(item);
                    db.SaveChanges();
                    TransactionItem transaction = mapper.Map<TransactionItem>(transactionItemDto);
                    db.Add(transaction);
                    db.SaveChanges();
                    responseDto.Result = mapper.Map<TransactionItemDto>(transaction);
                    responseDto.IsSuccess = true;
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.ErrorMessage = "Item are out of stock.";
                }

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
