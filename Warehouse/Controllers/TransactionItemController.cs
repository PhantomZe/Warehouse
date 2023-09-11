using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Warehouse.Models;
using Warehouse.Service.IService;
using Warehouse.Utility;

namespace Warehouse.Controllers
{
    public class TransactionItemController : Controller
    {
        private readonly IItemService itemService;
        private readonly ITransactionItemService transactionItemService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public TransactionItemController(IItemService itemService, ITransactionItemService transactionItemService, IHttpContextAccessor httpContextAccessor)
        {
            this.itemService = itemService;
            this.transactionItemService = transactionItemService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> TransactionIndex()
        {
            List<ItemDto> listItem = new List<ItemDto>();

            ResponseDto response = await itemService.GetAllItemsAsync();

            if (response != null && response.IsSuccess)
            {
                listItem = JsonConvert.DeserializeObject<List<ItemDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.ErrorMessage;
            }
            return View(listItem);
        }

		public async Task<IActionResult> Order(int id = 0)
		{
			OrderItem orderItem = new OrderItem();
			if (id != 0)
			{
				ResponseDto? response = await itemService.GetItemByIdAsync(id);

				if (response != null && response.IsSuccess)
				{
					ItemDto? model = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(response.Result));
                    orderItem.ItemID = model.ItemID;
                    orderItem.ItemName = model.ItemName;
                    orderItem.RemainingItemAmount = model.ItemAmount;
                    return View(orderItem);
				}
				else
				{
					TempData["error"] = response?.ErrorMessage;
				}
			}

			return View(orderItem);
		}

        public async Task<IActionResult> OrderList()
        {
            string? status = httpContextAccessor.HttpContext.Session.GetString("status");
            string? userID = httpContextAccessor.HttpContext.Session.GetString("ID");
            List<TransactionListItemDto> listTransaction = new List<TransactionListItemDto>();
            if (!string.IsNullOrEmpty(userID))
            {
                ResponseDto response = await transactionItemService.GetAllTransactionItemsAsync();
                listTransaction = JsonConvert.DeserializeObject<List<TransactionListItemDto>>(Convert.ToString(response.Result));
                if (status.Equals("1")) 
                {
                    listTransaction = listTransaction.Where(x => x.UserID.ToString().Equals(userID)).ToList();
                }
            }

            return View(listTransaction);
        }

        [HttpPost]
        public async Task<IActionResult> Order(OrderItem model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TransactionItemDto transactionItemDto = new TransactionItemDto
                    {
                        TransactionItemID = 0,
                        ItemID = model.ItemID,
                        ItemAmount = model.ItemAmount,
                        ItemPrice = model.ItemPrice,
                        UserID = model.UserID,
                        RecipientName = model.RecipientName,
                        TransactionDate = DateTime.Now
                    };
                    ResponseDto? response = await transactionItemService.InsertTransactionItemAsync(transactionItemDto);

                    if (response != null && response.IsSuccess)
                    {
                        TempData["success"] = "Item created successfully";
                        return RedirectToAction("TransactionIndex");
                    }
                    else
                    {
                        TempData["error"] = response?.ErrorMessage;
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View(model);
        }
    }
}
