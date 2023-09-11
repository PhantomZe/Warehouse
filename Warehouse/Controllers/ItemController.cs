using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using Warehouse.Models;
using Warehouse.Service.IService;
using Warehouse.Utility;

namespace Warehouse.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService itemService;
        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }
        public async Task<IActionResult> ItemIndex()
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
		public async Task<IActionResult> InsertUpdateItem(int id=0)
		{
			var statusList = new List<SelectListItem>()
			{
				new SelectListItem{Text=SD.StatusActive,Value="1"},
				new SelectListItem{Text=SD.StatusNonActive,Value="0"},
			};
			ViewBag.statusList = statusList;
			ViewBag.statusItem = "Create";
			ItemDto itemDto = new ItemDto();
			if (id!=0)
			{
				ResponseDto? response = await itemService.GetItemByIdAsync(id);

				if (response != null && response.IsSuccess)
				{
					ViewBag.statusItem = "Update";
					ItemDto? model = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(response.Result));
					return View(model);
				}
				else
				{
					TempData["error"] = response?.ErrorMessage;
				}
			}

			return View(itemDto);
		}

		[HttpPost]
		public async Task<IActionResult> ItemDelete(int id)
		{
			ResponseDto? response = await itemService.DeleteItemAsync(id);

			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "item deleted successfully";
				return RedirectToAction("ItemIndex");
			}
			else
			{
				TempData["error"] = response?.ErrorMessage;
			}
			return RedirectToAction("ItemIndex");
		}

		[HttpPost]
		public async Task<IActionResult> InsertUpdateItem(ItemDto model)
        {
            model.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
			{
				ResponseDto? response = await itemService.InsertUpdateItemsAsync(model);

				if (response != null && response.IsSuccess)
				{
					TempData["success"] = "Item created successfully";
					return RedirectToAction("ItemIndex");
				}
				else
				{
					TempData["error"] = response?.ErrorMessage;
				}
			}
			var statusList = new List<SelectListItem>()
			{
				new SelectListItem{Text=SD.StatusActive,Value="1"},
				new SelectListItem{Text=SD.StatusNonActive,Value="0"},
			};

			ViewBag.statusList = statusList;
			return View(model);
		}
	}
}
